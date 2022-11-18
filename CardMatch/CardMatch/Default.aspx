<%@ Page Title="Card Match" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CardMatch._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<style>
		.link_btn {
			box-shadow: 0px 10px 14px -7px #276873;
			background:linear-gradient(to bottom, #599bb3 5%, #408c99 100%);
			background-color:#599bb3;
			border-radius:8px;
			display:inline-block;
			cursor:pointer;
			color:#ffffff;
			font-family:Arial;
			font-size:16px;
			font-weight:bold;
			padding:13px 32px;
			text-decoration:none;
			text-shadow:0px 1px 0px #3d768a;
		}
		.link_btn:hover {
			background:linear-gradient(to bottom, #408c99 5%, #599bb3 100%);
			background-color:#408c99;
		}
		.link_btn:active {
			position:relative;
			top:1px;
		}
		.card{
		  height: 80px;
		  width: 80px;
		}
        .col-xs-4{
            padding-right:7px;
            padding-left:7px;
        }

	</style>
    <h1 style="text-align: center;">消消樂</h1>
    <hr />
    <div class="container-fluid">
	<div class="row">
		<div class="col-xs-12">
            <div class="col-xs-4">
                <a id="btn_lv_1" class="link_btn btn_lv">簡單</a>
            </div>
            <div class="col-xs-4">
                <a id="btn_lv_2" class="link_btn btn_lv">中等</a>			
            </div>
            <div class="col-xs-4">
                <a id="btn_lv_3" class="link_btn btn_lv">困難</a>
            </div>
		</div>
	</div>
    <br />
	<div class="row">
		<div class="col-xs-12">
            <div class="col-xs-4">
			    <a id="btn_hint" class="link_btn">提示</a>
		    </div>
			<div class="col-xs-4">
			    <a id="btn_reset" class="link_btn">重新</a>	
		    </div>
            <div class="col-xs-4" style="vertical-align: middle; padding: 12px;">
			    <label for="cars">Category:</label>
                <select name="category" id="category">
                  <option value="fruit">Fruit</option>
                  <option value="a-z">A-Z</option>
                </select>
		    </div>
		</div>
	</div>
	<hr />
	<div class="row">
		<div class="col-xs-12">
			<label id="lbl_ftimer">時間：</label>
			<span class="time_slot" id="hr">00</span>:
			<span class="time_slot" id="min">00</span>:
			<span class="time_slot" id="sec">00</span>.
			<span class="time_slot" id="cSec">00</span>
		</div>
	</div>
    <div class="row">
		<div class="col-xs-4">
			<label id="lbl_right">答對：</label>
			<span id="span_right"></span>
		</div>
		<div class="col-xs-4">
			<label id="lbl_wrong">答錯：</label>
			<span id="span_wrong"></span>
		</div>
		<div class="col-xs-4">
			<label id="lbl_hint">提示：</label>
			<span id="span_hint"></span>
		</div>
	</div>
	<div class="row">
		<div class="col-xs-12">
			<div class="row">
				<div class="col-xs-3 card" id = "card01">
					<input type="checkbox" id="chk01" />
				</div>
				<div class="col-xs-3 card" id = "card02">
					<input type="checkbox" id="chk02" />
				</div>
				<div class="col-xs-3 card" id = "card03">
					<input type="checkbox" id="chk03" />
				</div>
				<div class="col-xs-3 card" id = "card04">
					<input type="checkbox" id="chk04" />
				</div>
			</div>
			<div class="row">
				<div class="col-xs-3 card" id = "card05">
					<input type="checkbox" id="chk05" />
				</div>
				<div class="col-xs-3 card" id = "card06">
					<input type="checkbox" id="chk06" />
				</div>
				<div class="col-xs-3 card" id = "card07">
					<input type="checkbox" id="chk07" />
				</div>
				<div class="col-xs-3 card" id = "card08">
					<input type="checkbox" id="chk08" />
				</div>
			</div>
			<div class="row">
				<div class="col-xs-3 card" id = "card09">
					<input type="checkbox" id="chk09" />
				</div>
				<div class="col-xs-3 card" id = "card10">
					<input type="checkbox" id="chk10" />
				</div>
				<div class="col-xs-3 card" id = "card11">
					<input type="checkbox" id="chk11" />
				</div>
				<div class="col-xs-3 card" id = "card12">
					<input type="checkbox" id="chk12" />
				</div>
			</div>
			<div class="row">
				<div class="col-xs-3 card" id = "card13">
					<input type="checkbox" id="chk13" />
				</div>
				<div class="col-xs-3 card" id = "card14">
					<input type="checkbox" id="chk14" />
				</div>
				<div class="col-xs-3 card" id = "card15">
					<input type="checkbox" id="chk15" />
				</div>
				<div class="col-xs-3 card" id = "card16">
					<input type="checkbox" id="chk16" />
				</div>
			</div>		 
			<div class="row">
				<label id="lbl_message" style="color:darkblue; font-size:16pt"></label>
			</div>
		</div>
	</div>
	<!-- Modal -->
	<div class="modal fade" id="modal-vocabulary" data-keyboard="false" data-backdrop="static">
		<div class="modal-dialog" role="document">
			<div class="modal-content">
				<div class="modal-header">
					<h3 class="modal-title" id="exampleModalLabel">Please select correct vocabulary.</h3>
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">&times;</span>
					</button>
				</div>
				<div class="modal-body">
					<div class="row">
						<div class="col-xs-12 card" id = "card_modal"></div>
					</div>
					<div class="row">
						<button type="button" class="btn_modal" id="btn_modal0">select1</button>
						<button type="button" class="btn_modal" id="btn_modal1">select2</button>
						<button type="button" class="btn_modal" id="btn_modal2">select3</button>
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
					<%--<button type="button" class="btn btn-primary">Save changes</button>--%>
				</div>
			</div>
		</div>
	</div>
</div>
    <script src="Scripts/cardmatchLogic.js"></script>
</asp:Content>