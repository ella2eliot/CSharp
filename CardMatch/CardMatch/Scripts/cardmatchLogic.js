// 記憶原始圖片是哪一張，蓋牌後大家都是背面
var _picRecord = [];
// 翻開的圖片
var _selectRecord = [];
// 翻開的位置
var _selectRecordId = [];
// 紀錄已經翻開的牌，避免重複點選
var _totalTable = [false, false, false, false, false, false, false, false
    , false, false, false, false, false, false, false, false];
// 答案清單
var _answers = [];
var _msg_Right = "Correct!!";
var _msg_Wrong = "Not correct!!";
var _answer = "";
var _right = 0;
var _wrong = 0;
var _hint = 0;
var _level = 0;
var _matching = false;
var _isFinished = true;
// timer params
var timer;
var h, m, s, c = 99;

$(function () {
    // hide check box
    $("input[type=checkbox]").hide();
    setScore();
});
// img onclick event
$(".card").click(function () {
    // 沒選難度點及頁面不該有反應        
    if (_level == 0) {
        return false;
    }
    // 卡片檢查後才能再點擊
    if (_matching) {
        return false;
    }

    var id = $(this)
    // 紀錄 html tag id 後兩碼
    var index = parseInt(id.attr("id").substr(-2));

    // 成功(點過)後不重覆計算
    if (_totalTable[index - 1]) {
        return false;
    }

    // 翻牌
    $(this).css("background-image", _picRecord[index - 1]);

    // 計算圖片勾選次數
    $(this).children().prop("checked", true);

    // 紀錄翻出來的位置
    if (!!_selectRecordId[0]) {
        // seconde selected
        if (_selectRecordId[0] != index) {
            _selectRecordId.push(index);
            _selectRecord.push($(this).css("background-image"));
        }
    } else {
        // first selected			
        _selectRecordId.push(index);
        _selectRecord.push($(this).css("background-image"));
    }

    // 紀錄翻出來的圖
    // 選兩張後比對
    if ($('.card').find('input[type=checkbox]:checked').length == 2) {
        matchCheck(_selectRecordId);
    }
});

function matchCheck(selectRecordId) {
    _matching = true;
    var select1 = _selectRecord[0];
    var select2 = _selectRecord[1];
    if (select1 == select2) {
        setTimeout(() => {
            _matching = false;
        }, 100);
        if (_level == 1) {
            _totalTable[selectRecordId[0] - 1] = true;
            _totalTable[selectRecordId[1] - 1] = true;
            $("#lbl_message").html(_msg_Right);
            _right += 1;
            afterCheck();
        } else {
            // popout modal to choose right vocabulary
            vocabularyModal(select1);
        }
    } else {
        if (_level == 1) {
            _wrong += 1;
        }
        setTimeout(() => {
            var wrong1 = selectRecordId[0];
            var wrong2 = selectRecordId[1];

            $("#card" + (wrong1 < 10 ? "0" + wrong1.toString() : wrong1.toString())).css("background-image", "url('/content/imgs/ff.jpg')");
            $("#card" + (wrong2 < 10 ? "0" + wrong2.toString() : wrong2.toString())).css("background-image", "url('/content/imgs/ff.jpg')");
            _matching = false;
        }, 1000);
        $("#lbl_message").html(_msg_Wrong);
        afterCheck();
    }
}

// implement vocabulary select item.
function vocabularyModal(selectedPic) {
    $("#modal-vocabulary").modal('show');
    $("#card_modal").css("background-image", selectedPic);

    // 隨選兩個選項+正確答案
    var ansIndex = $("#card_modal").css("background-image").substr(-8, 2) - 1;
    var item1 = getRandom(1, 26);
    item1 = item1 == ansIndex ? getRandom(1, 26) : item1;
    var item2 = getRandom(1, 26);
    item2 = item2 == ansIndex ? getRandom(1, 26) : item2;

    // 打散選項
    var itemSort = getRandom(1, 99) % 3;
    _answer = _answers[ansIndex];
    if (itemSort == 0) {
        $("#btn_modal0").html(_answers[item1]);
        $("#btn_modal1").html(_answers[item2]);
        $("#btn_modal2").html(_answers[ansIndex]);
    } else if (itemSort == 1) {
        $("#btn_modal0").html(_answers[item1]);
        $("#btn_modal1").html(_answers[ansIndex]);
        $("#btn_modal2").html(_answers[item2]);
    } else {
        $("#btn_modal0").html(_answers[ansIndex]);
        $("#btn_modal1").html(_answers[item1]);
        $("#btn_modal2").html(_answers[item2]);
    }
}

// vocabulary answer action.
$(".btn_modal").click(function () {
    var selected = $(this).html();
    if (selected == _answer) {
        _right += 1;
        _totalTable[_selectRecordId[0] - 1] = true;
        _totalTable[_selectRecordId[1] - 1] = true;
        $("#lbl_message").html(_msg_Right);
    } else {
        _wrong += 1;
        $("#lbl_message").html(_msg_Wrong);

        // 把牌蓋回去
        var wrong1 = _selectRecordId[0];
        var wrong2 = _selectRecordId[1];

        $("#card" + (wrong1 < 10 ? "0" + wrong1.toString() : wrong1.toString())).css('background-image', "url('/content/imgs/ff.jpg')");
        $("#card" + (wrong2 < 10 ? "0" + wrong2.toString() : wrong2.toString())).css('background-image', "url('/content/imgs/ff.jpg')");
    }
    afterCheck();
    $("#modal-vocabulary").modal('hide');
});

$('#modal-vocabulary').on('hidden.bs.modal', function () {
    // 單字錯誤的話把牌蓋回去
    var wrong1 = _selectRecordId[0];
    var wrong2 = _selectRecordId[1];

    $("#card" + (wrong1 < 10 ? "0" + wrong1.toString() : wrong1.toString())).css('background-image', "url('/content/imgs/ff.jpg')");
    $("#card" + (wrong2 < 10 ? "0" + wrong2.toString() : wrong2.toString())).css('background-image', "url('/content/imgs/ff.jpg')");

    afterCheck();
});

function afterCheck() {

    // fade correct pic
    if ($("#lbl_message").html() == _msg_Right) {
        $("#card" + (_selectRecordId[0] < 10 ? "0" + _selectRecordId[0].toString() : _selectRecordId[0].toString())).css("opacity", "20%");
        $("#card" + (_selectRecordId[1] < 10 ? "0" + _selectRecordId[1].toString() : _selectRecordId[1].toString())).css("opacity", "20%");
    }

    // stop timmer when all matched
    if (_right == 8) {
        clearInterval(timer);
        _isFinished = true;
    }
    // update UI info
    setScore();
    // reset select record;
    _selectRecord = [];
    _selectRecordId = [];
    // uncheck select record;
    $("div>input").prop("checked", false);
}

function isFinished() {
    var result = true;
    _totalTable.forEach(function (item) {
        if (!item) {
            return false;
        }
    });
    return result;
}

// button setting
$("#btn_lv_1").click(function () {
    if (!_isFinished) {
        return false;
    }
    reset();
    _level = 1;
    implement(_level);
});

$("#btn_lv_2").click(function () {
    if (!_isFinished) {
        return false;
    }
    reset();
    _level = 2;
    implement(_level);
});

$("#btn_lv_3").click(function () {
    if (!_isFinished) {
        return false;
    }
    reset();
    _level = 3;
    implement(_level);
});

$("#btn_hint").click(function () {
    if (_level == 0) {
        return false;
    }
    if (_right == 8) {
        return false;
    }
    _hint += 1;
    setScore();

    // 打開
    const cardDiv = document.getElementsByClassName('card');
    [...cardDiv].forEach((el, index) => {
        $("#card" + (index < 9 ? "0" + (index + 1) : (index + 1))).css("background-image", _picRecord[index]);
    });

    // 關閉
    setTimeout(() => {
        for (var i = 0; i < _totalTable.length; i++) {
            if (!_totalTable[i]) {
                $("#card" + (i < 9 ? "0" + (i + 1) : (i + 1))).css("background-image", "url('/content/imgs/ff.jpg')");
            }
        }
    }, 1000);

    // 避免選一張後按提示，需要清除選過的選項
    _selectRecord = [];
    _selectRecordId = [];
    // uncheck all checkbox
    $("div>input").prop('checked', false);
});

$("#btn_reset").click(function () {
    reset();
});

// 實作隨機分配
function implement(level) {
    _matching = true;
    _isFinished = false;

    // level3 timer init: limit one minute
    if (_level == 3) {
        $("#min").html("01");
    }

    // img start from f{picno+2}.jpg
    var picno = -1;

    if ($("#category").val() == "fruit") {
        _answers = ["Lemon", "Start fruit", "Orange", "Strawberry", "Cherry", "Litchi", "Kiwi", "Apple",
            "Lemon", "Tomato", "Banana", "Orange", "Strawberry", "Mangosteen", "Watermelon", "Pear",
            "Cantaloupe", "Grape", "Pineapple", "Cherry", "Orange", "Kiwi", "Pear", "Mango", "Pitaya", "Apple"];
    } else {
        _answers = ["Ant", "Bird", "Cup", "Duck", "Elephant", "Fan", "Girl", "Hat"
            , "Igloo", "Jam", "Key", "Lamp", "Mom", "Net", "Octopus", "Pig", "Queen"
            , "Rabbit", "Snake", "Turtle", "Umbrella", "Van", "Watch", "Fox", "yo-yo", "Zebra"]
    }
    picno += getRandom(1, 26);

    // set background image.
    const cardDiv = document.getElementsByClassName('card');
    [...cardDiv].forEach((el, index) => {
        picno = index % 2 == 0 ? picno + 1 : picno;
        if (picno > 25) {
            picno = 1;
        }
        var url = "url('/content/imgs/" + $("#category").val() + "/f" + (picno < 9 ? "0" + (picno + 1) : (picno + 1)) + ".jpg')";
        $("#" + el.id).css('background-image', url);
        // url('/content/imgs/f1.jpg')
        $("#" + el.id).css('background-size', "contain");
        $("#" + el.id).css('background-repeat', "no-repeat");
        $("#" + el.id).css('background-position', "center");
    });

    // random switch with each

    for (var i = 0; i < 256; i++) {
        var t1 = getRandom(1, 16);
        var t2 = getRandom(1, 16);
        var div_1 = t1 < 9 ? "0" + t1 : t1;
        var div_2 = t2 < 9 ? "0" + t2 : t2;
        var pic_1 = $("#card" + div_1).css("background-image");
        var pic_2 = $("#card" + div_2).css("background-image");
        $("#card" + div_1).css('background-image', pic_2);
        $("#card" + div_2).css('background-image', pic_1);
    }

    // record image location
    [...cardDiv].forEach((el, index) => {
        _picRecord.push($("#card" + (index < 9 ? "0" + (index + 1) : (index + 1))).css("background-image"));
    });

    // turn around all image
    setTimeout(() => {
        if (!_isFinished) {
            setAllImage("ff")
            _matching = false;
            // start timmer
            startRestartInterval();
        }
    }, 3000);
}

//產生min到max之間的亂數
function getRandom(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
};

// 重設
function reset() {
    // reset images
    setAllImage("bb");
    // reset record
    _picRecord = [];
    // uncheck all checkbox
    $("div>input").prop('checked', false);
    // reset score
    _right = _wrong = _level = _hint = 0;
    setScore();
    _matching = false;
    _isFinished = true;
    // reset select record
    _totalTable = [false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false];
    _selectRecord = [];
    _selectRecordId = [];
    // enable level button
    $(".btn_lv").removeAttr("disabled");

    // reset timmer
    clearInterval(timer);
    h = m = s = c = 0;
    $(".time_slot").text("00");

    // reset lbl_message
    $("#lbl_message").html("");

    // reset opacity
    $(".card").css("opacity", "100%");

}

function setScore() {
    $("#span_right").html(_right);
    $("#span_wrong").html(_wrong);
    $("#span_hint").html(_hint);
}

function setAllImage(pic) {
    const cardDiv = document.getElementsByClassName('card');
    [...cardDiv].forEach((el, index) => {
        $("#" + el.id).css('background-image', "url('/content/imgs/" + pic + ".jpg')");
    });
}

// timer Functions
function startRestartInterval() {
    timer = setInterval(function () {
        if (c < 99) {
            c++;
        }
        else {
            c = 0;
            if (s < 59) {
                s++;
            } else {
                s = 0;
                if (m < 59) {
                    m++;
                } else {
                    m = 0;
                    if (h < 59) {
                        h++;
                    }
                }
            }
        }
        $("span").eq(3).text((h < 10) ? ("0" + h) : h);
        $("span").eq(4).text((m < 10) ? ("0" + m) : m);
        $("span").eq(5).text((s < 10) ? ("0" + s) : s);
        $("span").eq(6).text((c < 10) ? ("0" + c) : c);
    }, 10);
}

function getValues() {
    h = parseInt($("#hr").text());
    m = parseInt($("#min").text());
    s = parseInt($("#sec").text());
    c = parseInt($("#cSec").text());
}

function countDown(elem, seconds) {
    var that = {};

    that.elem = elem;
    that.seconds = seconds;
    that.totalTime = seconds * 100;
    that.usedTime = 0;
    that.startTime = +new Date();
    that.timer = null;

    that.count = function () {
        that.usedTime = Math.floor((+new Date() - that.startTime) / 10);

        var tt = that.totalTime - that.usedTime;
        if (tt <= 0) {
            that.elem.innerHTML = '00:00.00';
            clearInterval(that.timer);
        } else {
            var mi = Math.floor(tt / (60 * 100));
            var ss = Math.floor((tt - mi * 60 * 100) / 100);
            var ms = tt - Math.floor(tt / 100) * 100;

            that.elem.innerHTML = that.fillZero(mi) + ":" + that.fillZero(ss) + "." + that.fillZero(ms);
        }
    };

    that.init = function () {
        if (that.timer) {
            clearInterval(that.timer);
            that.elem.innerHTML = '00:00.00';
            that.totalTime = seconds * 100;
            that.usedTime = 0;
            that.startTime = +new Date();
            that.timer = null;
        }
    };
    that.start = function () {
        if (!that.timer) {
            that.timer = setInterval(that.count, 1);
        }
    };

    that.stop = function () {
        console.log('usedTime = ' + countdown.usedTime);
        if (that.timer) clearInterval(that.timer);
    };

    that.fillZero = function (num) {
        return num < 10 ? '0' + num : num;
    };

    return that;
}