<%
Rem �ʺ��׾ª����ҽX�{��
Rem �ʺ�7.1���ҽX�r���V�q�w���X�i

Rem �o�ӵ{���u�O�����A��@�̦ۤv��{���V�q�Ϲ��ܤ�
Rem �ϥήɡA�Ш�49���� image_code ����ڪ�session�W��

Rem NetDust ��F�X�Ӥp�a��
Rem 2006-3-9 �ץ��F�@�ӥi�ణ0�����~
Rem 2006-7-31 �r���Ӽ��H���A�e�סB���סB��m�H���A�[�J�z�Z�u


Rem ---------------------

Option Explicit

Rem �]�m����

Const nSaturation = 100     ' ��m���M��
Const nBlankNoisyDotOdds = 0.02  ' �ťճB���I�v
Const nColorNoisyDotOdds = 0.0  ' ����B���I�v
Const nNoisyLineCount = 4    ' �����u����
Const nCharMin = 8       ' ���ͪ��̤p�r���Ӽ�
Const nCharMax = 8       ' ���ͪ��̤j�r���Ӽ�
Const nSpaceX = 2        ' ���k���䪺�ťռe��
Const nSpaceY = 2        ' �W�U���䪺�ťռe��
Const nImgWidth = 120      ' �ƾڰϼe��
Const nImgHeight = 35      ' ��ưϰ���
Const nCharWidthRandom = 16   ' �r���e���H���q
Const nCharHeightRandom = 16  ' �r�������H���q
Const nPositionXRandom = 10   ' ��V��m�H���q
Const nPositionYRandom = 10   ' �a�V��m�H���q
Const nAngleRandom = 6          ' ���������H���q
Const nLengthRandom = 6         ' ���������H���q(�ʤ���)
Const nColorHue = -2      ' ������ҽX�����(-1����H�����, -2��ܦǫצ��)
Const cCharSet = "123456789BCDHIJKLMNPRX"
                                ' �c�����ҽX���r����
                                ' �p�G�X�R�F�U�䪺�r���V�q�w�A�h�i�H�����X�R�o�Ӧr����

Rem ---------------------

Response.Expires = -9999  
Response.AddHeader "Pragma","no-cache" 
Response.AddHeader "cache-ctrol","no-cache" 
Response.ContentType = "Image/BMP" 
Randomize

Dim Buf(), DigtalStr
Dim Lines(), LineCount
Dim CursorX, CursorY, DirX, DirY, nCharCount, nPixelWidth, nPixelHeight, PicWidth, PicHeight
nCharCount = nCharMin + CInt(Rnd * (nCharMax - nCharMin))
PicWidth = nImgWidth + 2 * nSpaceX
PicHeight = nImgHeight + 2 * nSpaceY

Call CreatValidCode("image_code")

Sub CDGen_Reset()
 ' ���w�V�q���M�����ܼ�
 LineCount = 0
 CursorX = 0
 CursorY = 0
 ' ��l��������V�O�����V�U
 DirX = 0
 DirY = 1
End Sub

Sub CDGen_Clear()
 ' �M�Ŧ�ϰ}�C
 Dim i, j
 ReDim Buf(PicHeight - 1, PicWidth - 1)

 For j = 0 To PicHeight - 1
  For i = 0 To PicWidth - 1
   Buf(j, i) = 0
  Next
 Next
End Sub

Sub CDGen_PSet(X, Y)
 ' �b�I�}�ϰ}�C�W�e�I
 If X >= 0 And X < PicWidth And Y >= 0 And Y < PicHeight Then Buf(Y, X) = 1
End Sub

Sub CDGen_Line(X1, Y1, X2, Y2)
 ' �b�I�}�ϰ}�C�W�e�u
 Dim DX, DY, DeltaT, i

 DX = X2 - X1
 DY = Y2 - Y1
 If Abs(DX) > Abs(DY) Then DeltaT = Abs(DX) Else DeltaT = Abs(DY)
 If DeltaT = 0 Then
  CDGen_PSet CInt(X1),CInt(Y1)
 Else
  For i = 0 To DeltaT
   CDGen_PSet CInt(X1 + DX * i / DeltaT), CInt(Y1 + DY * i / DeltaT)
  Next
 End If
End Sub

Sub CDGen_FowardDraw(nLength)
 ' ����e������Vø�s���w���רò��ʥ����A���ƪ�ܱq���V�k/�q�W�V�Uø�s�A�t�ƪ�ܱq�k�V��/�q�U�V�Wø�s
 nLength = nLength * (1 + (Rnd * 2 - 1) * nLengthRandom / 100)
 ReDim Preserve Lines(3, LineCount)
 Lines(0, LineCount) = CursorX
 Lines(1, LineCount) = CursorY
 CursorX = CursorX + DirX * nLength
 CursorY = CursorY + DirY * nLength
 Lines(2, LineCount) = CursorX
 Lines(3, LineCount) = CursorY
 LineCount = LineCount + 1
End Sub

Sub CDGen_SetDirection(nAngle)
 ' �����w���׳]�w�e����V, ���ƪ�ܬ۹��e��V���ɰw���ܤ�V�A�t�ƪ�ܬ۹��e��V�f�ɰw���ܤ�V
 Dim DX, DY

 nAngle = (nAngle + (Rnd * 2 - 1) * nAngleRandom) / 180 * 3.1415926
 DX = DirX
 DY = DirY
 DirX = DX * Cos(nAngle) - DY * Sin(nAngle)
 DirY = DX * Sin(nAngle) + DY * Cos(nAngle)
End Sub

Sub CDGen_MoveToMiddle(nActionIndex, nPercent)
 ' �N�e����в��ʨ���w�ʧ@�������I�W�AnPercent��������m���ʤ���
 Dim DeltaX, DeltaY

 DeltaX = Lines(2, nActionIndex) - Lines(0, nActionIndex)
 DeltaY = Lines(3, nActionIndex) - Lines(1, nActionIndex)
 CursorX = Lines(0, nActionIndex) + DeltaX * nPercent / 100
 CursorY = Lines(1, nActionIndex) + DeltaY * Abs(DeltaY) * nPercent / 100
End Sub

Sub CDGen_MoveCursor(nActionIndex)
 ' �N�e����в��ʨ���w�ʧ@���_�l�I�W
 CursorX = Lines(0, nActionIndex)
 CursorY = Lines(1, nActionIndex)
End Sub

Sub CDGen_Close(nActionIndex)
 ' �N��e������m�P���w�ʧ@���_�l�I���X�ò��ʥ���
 ReDim Preserve Lines(3, LineCount)
 Lines(0, LineCount) = CursorX
 Lines(1, LineCount) = CursorY
 CursorX = Lines(0, nActionIndex)
 CursorY = Lines(1, nActionIndex)
 Lines(2, LineCount) = CursorX
 Lines(3, LineCount) = CursorY
 LineCount = LineCount + 1
End Sub

Sub CDGen_CloseToMiddle(nActionIndex, nPercent)
 ' �N��e������m�P���w�ʧ@�������I���X�ò��ʥ����AnPercent��������m���ʤ���
 Dim DeltaX, DeltaY

 ReDim Preserve Lines(3, LineCount)
 Lines(0, LineCount) = CursorX
 Lines(1, LineCount) = CursorY
 DeltaX = Lines(2, nActionIndex) - Lines(0, nActionIndex)
 DeltaY = Lines(3, nActionIndex) - Lines(1, nActionIndex)
 CursorX = Lines(0, nActionIndex) + Sgn(DeltaX) * Abs(DeltaX) * nPercent / 100
 CursorY = Lines(1, nActionIndex) + Sgn(DeltaY) * Abs(DeltaY) * nPercent / 100
 Lines(2, LineCount) = CursorX
 Lines(3, LineCount) = CursorY
 LineCount = LineCount + 1
End Sub

Sub CDGen_Flush(X0, Y0)
 ' ���ӷ�e���e���ʧ@�ǦCø�s�I�}���I�}
 Dim MaxX, MinX, MaxY, MinY
 Dim DeltaX, DeltaY, StepX, StepY, OffsetX, OffsetY
 Dim i

 MaxX = MinX = MaxY = MinY = 0
 For i = 0 To LineCount - 1
  If MaxX < Lines(0, i) Then MaxX = Lines(0, i)
  If MaxX < Lines(2, i) Then MaxX = Lines(2, i)
  If MinX > Lines(0, i) Then MinX = Lines(0, i)
  If MinX > Lines(2, i) Then MinX = Lines(2, i)
  If MaxY < Lines(1, i) Then MaxY = Lines(1, i)
  If MaxY < Lines(3, i) Then MaxY = Lines(3, i)
  If MinY > Lines(1, i) Then MinY = Lines(1, i)
  If MinY > Lines(3, i) Then MinY = Lines(3, i)
 Next
 DeltaX = MaxX - MinX
 DeltaY = MaxY - MinY
 If DeltaX = 0 Then DeltaX = 1
 If DeltaY = 0 Then DeltaY = 1
 MaxX = MinX
 MaxY = MinY
 If DeltaX > DeltaY Then
  StepX = (nPixelWidth - 2) / DeltaX
  StepY = (nPixelHeight - 2) / DeltaX
  OffsetX = 0
  OffsetY = (DeltaX - DeltaY) / 2
 Else
  StepX = (nPixelWidth - 2) / DeltaY
  StepY = (nPixelHeight - 2) / DeltaY
  OffsetX = (DeltaY - DeltaX) / 2
  OffsetY = 0
 End If
 For i = 0 To LineCount - 1
  Lines(0, i) = Round((Lines(0, i) - MaxX + OffsetX) * StepX, 0)
  Lines(1, i) = Round((Lines(1, i) - MaxY + OffsetY) * StepY, 0)
  Lines(2, i) = Round((Lines(2, i) - MinX + OffsetX) * StepX, 0)
  Lines(3, i) = Round((Lines(3, i) - MinY + OffsetY) * StepY, 0)
  CDGen_Line Lines(0, i) + X0 + 1, Lines(1, i) + Y0 + 1, Lines(2, i) + X0 + 1, Lines(3, i) + Y0 + 1
 Next
End Sub

Sub CDGen_SetDirectionFormZero(nAngle)
 '�����w���׳]�w�e����V�A�PCDGen_SetDirection���ϧO�O�H0�׬����
  nAngle = Sgn(nAngle) * (Abs(nAngle) - nAngleRandom + Rnd * nAngleRandom * 2) / 180 * 3.1415926
  DirX = - Sin(nAngle)
  DirY = Cos(nAngle)
End Sub

Sub CDGen_Char(cChar, X0, Y0)
 ' �b���w�y�гB�ͦ����w�r�����I�}�ϰ}�C
 CDGen_Reset
 Select Case cChar
 Case "0"
  CDGen_SetDirection -60                            ' �f�ɰw60��(�۹�󫫪��u)
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' �f�ɰw60��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 1.5                              ' ø�s1.5�ӳ��
  CDGen_SetDirection -60                            ' �f�ɰw60��
  CDGen_FowardDraw 0.7                              ' ø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' ���ɰw120��
  CDGen_FowardDraw 0.7                              ' ø�s0.7�ӳ��
  CDGen_Close 0                                     ' �ʳ���e���P��0��(0�}�l)
 Case "1"
  CDGen_SetDirection -90                            ' �f�ɰw90��(�۹�󫫪��u)
  CDGen_FowardDraw 0.5                              ' ø�s0.5�ӳ��
  CDGen_MoveToMiddle 0, 50                          ' ���ʵe������m���0��(0�}�l)��50%�B
  CDGen_SetDirection 90                             ' �f�ɰw90��
  CDGen_FowardDraw -1.4                             ' �Ϥ�Vø�s1.4�ӳ��
  CDGen_SetDirection 30                             ' �f�ɰw30��
  CDGen_FowardDraw 0.4                              ' ø�s0.4�ӳ��
 Case "2"
  CDGen_SetDirection 45                             ' ���ɰw45��(�۹�󫫪��u)
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -120                           ' �f�ɰw120��
  CDGen_FowardDraw 0.4                              ' ø�s0.4�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.6                              ' ø�s0.6�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 1.6                              ' ø�s1.6�ӳ��
  CDGen_SetDirection -135                           ' �f�ɰw135��
  CDGen_FowardDraw 1.0                              ' ø�s1.0�ӳ��
 Case "3"
  CDGen_SetDirection -90                            ' �f�ɰw90��(�۹�󫫪��u)
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection 135                            ' ���ɰw135��
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection -120                           ' �f�ɰw120��
  CDGen_FowardDraw 0.6                              ' ø�s0.6�ӳ��
  CDGen_SetDirection 80                             ' ���ɰw80��
  CDGen_FowardDraw 0.5                              ' ø�s0.5�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.5                              ' ø�s0.5�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.5                              ' ø�s0.5�ӳ��
 Case "4"
  CDGen_SetDirection 20                             ' ���ɰw20��(�۹�󫫪��u)
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection -110                           ' �f�ɰw110��
  CDGen_FowardDraw 1.2                              ' ø�s1.2�ӳ��
  CDGen_MoveToMiddle 1, 60                          ' ���ʵe������m���1��(0�}�l)��60%�B
  CDGen_SetDirection 90                             ' ���ɰw90��
  CDGen_FowardDraw 0.7                              ' ø�s0.7�ӳ��
  CDGen_MoveCursor 2                                ' ���ʵe�����2��(0�}�l)���}�l�B
  CDGen_FowardDraw -1.5                             ' �Ϥ�Vø�s1.5�ӳ��
 Case "5"
  CDGen_SetDirection 90                             ' ���ɰw90��(�۹�󫫪��u)
  CDGen_FowardDraw 1.0                              ' ø�s1.0�ӳ��
  CDGen_SetDirection -90                            ' �f�ɰw90��
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection -90                            ' �f�ɰw90��
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection 30                             ' ���ɰw30��
  CDGen_FowardDraw 0.4                              ' ø�s0.4�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.4                              ' ø�s0.4�ӳ��
  CDGen_SetDirection 30                             ' ���ɰw30��
  CDGen_FowardDraw 0.5                              ' ø�s0.5�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
 Case "6"
  CDGen_SetDirection -60                            ' �f�ɰw60��(�۹�󫫪��u)
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' �f�ɰw60��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 1.5                              ' ø�s1.5�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 0.7                              ' ø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 0.5                              ' ø�s0.5�ӳ��
  CDGen_CloseToMiddle 2, 50                         ' �N��e�e����m�P��2��(0�}�l)��50%�B�ʳ�
 Case "7"
  CDGen_SetDirection 180                            ' ���ɰw180��(�۹�󫫪��u)
  CDGen_FowardDraw 0.3                              ' ø�s0.3�ӳ��
  CDGen_SetDirection 90                             ' ���ɰw90��
  CDGen_FowardDraw 0.9                              ' ø�s0.9�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 1.3                              ' ø�s1.3�ӳ��
 Case "8"
  CDGen_SetDirection -60                            ' �f�ɰw60��(�۹�󫫪��u)
  CDGen_FowardDraw -0.8                             ' �Ϥ�Vø�s0.8�ӳ��
  CDGen_SetDirection -60                            ' �f�ɰw60��
  CDGen_FowardDraw -0.8                             ' �Ϥ�Vø�s0.8�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection 110                            ' ���ɰw110��
  CDGen_FowardDraw -1.5                             ' �Ϥ�Vø�s1.5�ӳ��
  CDGen_SetDirection -110                           ' �f�ɰw110��
  CDGen_FowardDraw 0.9                              ' ø�s0.9�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.8                              ' ø�s0.8�ӳ��
  CDGen_SetDirection 60                             ' ���ɰw60��
  CDGen_FowardDraw 0.9                              ' ø�s0.9�ӳ��
  CDGen_SetDirection 70                             ' ���ɰw70��
  CDGen_FowardDraw 1.5                             ' ø�s1.5�ӳ��
  CDGen_Close 0                                     ' �ʳ���e���P��0��(0�}�l)
 Case "9"
  CDGen_SetDirection 120                            ' �f�ɰw60��(�۹�󫫪��u)
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' �f�ɰw60��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' ���ɰw120��
  CDGen_FowardDraw -1.5                              ' ø�s1.5�ӳ��
  CDGen_SetDirection -60                            ' ���ɰw120��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' ���ɰw120��
  CDGen_FowardDraw -0.7                              ' ø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' ���ɰw120��
  CDGen_FowardDraw 0.5                              ' ø�s0.5�ӳ��
  CDGen_CloseToMiddle 2, 50                         ' �N��e�e����m�P��2��(0�}�l)��50%�B�ʳ�
 ' �H�U���r�����V�q�ʧ@�A�����쪺�i�H�~��
 Case "A"
  CDGen_SetDirection -(Rnd * 20 + 150)              ' �f�ɰw150-170��(�۹�󫫪��u)
  CDGen_FowardDraw Rnd * 0.2 + 1.1                  ' ø�s1.1-1.3�ӳ��
  CDGen_SetDirection Rnd * 20 + 140                 ' ���ɰw140-160��
  CDGen_FowardDraw Rnd * 0.2 + 1.1                  ' ø�s1.1-1.3�ӳ��
  CDGen_MoveToMiddle 0, 30                          ' ���ʵe������m���1��(0�}�l)��30%�B
  CDGen_CloseToMiddle 1, 70                         ' �N��e�e����m�P��1��(0�}�l)��70%�B�ʳ�
 Case "B"
  CDGen_SetDirection -(Rnd * 20 + 50)               ' �f�ɰw50-70��(�۹�󫫪��u)
  CDGen_FowardDraw Rnd * 0.4 + 0.8                  ' ø�s0.8-1.2�ӳ��
  CDGen_SetDirection Rnd * 20 + 110                 ' ���ɰw110-130��
  CDGen_FowardDraw Rnd * 0.2 + 0.6                  ' ø�s0.6-0.8�ӳ��
  CDGen_SetDirection -(Rnd * 20 + 110)              ' �f�ɰw110-130��
  CDGen_FowardDraw Rnd * 0.2 + 0.6                  ' ø�s0.6-0.8�ӳ��
  CDGen_SetDirection Rnd * 20 + 110                 ' ���ɰw110-130��
  CDGen_FowardDraw Rnd * 0.4 + 0.8                  ' ø�s0.8-1.2�ӳ��
  CDGen_Close 0                                     ' �ʳ���e���P��1��(0�}�l)
 Case "C"
  CDGen_SetDirection -60                            ' �f�ɰw60��(�۹�󫫪��u)
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection -60                            ' �f�ɰw60��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 1.5                              ' ø�s1.5�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw -0.7                             ' �Ϥ�Vø�s0.7�ӳ��
  CDGen_SetDirection 120                            ' ���ɰw120��
  CDGen_FowardDraw 0.7                              ' ø�s0.7�ӳ��
 Case "D"
   CDGen_SetDirection -(Rnd * 40 - 20)
   CDGen_FowardDraw 1
   CDGen_SetDirection -120
   CDGen_FowardDraw 0.4
   CDGen_SetDirection -45
   CDGen_FowardDraw 0.5
   CDGen_Close 0
 Case "E"
   CDGen_SetDirection -(Rnd * 20 - 10)
   CDGen_FowardDraw 1
   CDGen_MoveToMiddle 0, 0
   CDGen_SetDirectionFormZero -(110 - Rnd * 40)
   CDGen_FowardDraw 0.7
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirectionFormZero -(110 - Rnd * 40)
   CDGen_FowardDraw 0.5
   CDGen_MoveToMiddle 0, 100
   CDGen_SetDirectionFormZero -(110 - Rnd * 40)
   CDGen_FowardDraw 0.9
 Case "F"
   CDGen_SetDirection -(Rnd * 20 - 10)
   CDGen_FowardDraw 1
   CDGen_MoveToMiddle 0, 0
   CDGen_SetDirectionFormZero -(110 - Rnd * 40)
   CDGen_FowardDraw 0.7
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirectionFormZero -(110 - Rnd * 40)
   CDGen_FowardDraw 0.5
 Case "G"
   CDGen_SetDirection -60
   CDGen_FowardDraw -0.7
   CDGen_SetDirection -60
   CDGen_FowardDraw -0.7
   CDGen_SetDirection 120
   CDGen_FowardDraw 1.5
   CDGen_SetDirection 120
   CDGen_FowardDraw -0.7
   CDGen_SetDirection 120
   CDGen_FowardDraw 0.7
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.5
   CDGen_SetDirection 90
   CDGen_FowardDraw 0.4
   CDGen_MoveToMiddle 6, 0
   CDGen_SetDirection 180
   CDGen_FowardDraw 0.4
 Case "H"
   CDGen_SetDirection -(Rnd * 20 - 10)
   CDGen_FowardDraw 1
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirection -90
   CDGen_FowardDraw 1
   CDGen_MoveToMiddle 1, 100
   CDGen_SetDirection -90
   CDGen_FowardDraw 0.5
   CDGen_MoveToMiddle 1, 100
   CDGen_SetDirection 180
   CDGen_FowardDraw 0.5
 Case "I"
   CDGen_SetDirection -(Rnd * 20 - 10)
   CDGen_FowardDraw 1
   CDGen_MoveToMiddle 0, 0
   CDGen_SetDirection -90
   CDGen_FowardDraw 0.2
   CDGen_MoveToMiddle 0, 0
   CDGen_SetDirection 180
   CDGen_FowardDraw 0.2
   CDGen_MoveToMiddle 0, 100
   CDGen_FowardDraw 0.2
   CDGen_MoveToMiddle 0, 100
   CDGen_SetDirection 180
   CDGen_FowardDraw 0.2
 Case "J"
   CDGen_SetDirection -90
   CDGen_FowardDraw 0.4
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirection 90
   CDGen_FowardDraw 0.6
   CDGen_SetDirection 60
   CDGen_FowardDraw 0.5
   CDGen_SetDirection 120
   CDGen_FowardDraw 0.5
 Case "K"
   CDGen_SetDirection -(Rnd * 20 - 10)
   CDGen_FowardDraw 1
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.6
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.6
 Case "L"
   CDGen_SetDirection -90
   CDGen_FowardDraw 0.2
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirectionFormZero -(Rnd * 20 - 10)
   CDGen_FowardDraw 1
   CDGen_SetDirection -(110 - Rnd * 40)
   CDGen_FowardDraw 0.8
   CDGen_SetDirectionFormZero 0
   CDGen_FowardDraw -0.3
 Case "M"
   CDGen_SetDirection 0
   CDGen_FowardDraw -1
   CDGen_SetDirection -30
   CDGen_FowardDraw 0.5
   CDGen_SetDirection 60
   CDGen_FowardDraw -0.5
   CDGen_SetDirection -30
   CDGen_FowardDraw 1
 Case "N"
   CDGen_SetDirection 0
   CDGen_FowardDraw -1
   CDGen_SetDirection -45
   CDGen_FowardDraw 1.4
   CDGen_SetDirection 45
   CDGen_FowardDraw -1
 Case "O"
   CDGen_SetDirection -60
   CDGen_FowardDraw -0.7
   CDGen_SetDirection -60
   CDGen_FowardDraw -0.7
   CDGen_SetDirection 120
   CDGen_FowardDraw 1.5
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.7
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.7
   CDGen_Close 0
 Case "P"
   CDGen_SetDirection 0
   CDGen_FowardDraw -1
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.5
   CDGen_SetDirection 60
   CDGen_FowardDraw 0.5
   CDGen_CloseToMiddle 0, 50
 Case "Q"
   CDGen_SetDirection -60
   CDGen_FowardDraw -0.7
   CDGen_SetDirection -60
   CDGen_FowardDraw -0.7
   CDGen_SetDirection 120
   CDGen_FowardDraw 1.5
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.7
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.7
   CDGen_Close 0
   CDGen_MoveToMiddle 4, 100
   CDGen_SetDirectionFormZero -45
   CDGen_FowardDraw 0.7
   CDGen_MoveToMiddle 4, 100
   CDGen_SetDirection 180
   CDGen_FowardDraw 0.7
 Case "R"
   CDGen_SetDirection 0
   CDGen_FowardDraw -1
   CDGen_SetDirection -80
   CDGen_FowardDraw 0.5
   CDGen_SetDirection 60
   CDGen_FowardDraw 0.5
   CDGen_CloseToMiddle 0, 50
   CDGen_SetDirectionFormZero -45
   CDGen_FowardDraw 0.7
 Case "S"
   CDGen_SetDirection -45
   CDGen_FowardDraw -0.5
   CDGen_SetDirection -90
   CDGen_FowardDraw -0.5
   CDGen_SetDirection 90
   CDGen_FowardDraw 1
   CDGen_SetDirection 90
   CDGen_FowardDraw 0.5
   CDGen_SetDirection 90
   CDGen_FowardDraw 0.5
 Case "T"
   CDGen_SetDirection -90
   CDGen_FowardDraw 0.8
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirection 90
   CDGen_FowardDraw 1
   CDGen_MoveToMiddle 0, 0
   CDGen_SetDirection 30
   CDGen_FowardDraw 0.5
   CDGen_MoveToMiddle 0, 100
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.5
 Case "U"
   CDGen_FowardDraw 1
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.6
   CDGen_SetDirection -60
   CDGen_FowardDraw 0.6
   CDGen_SetDirection -60
   CDGen_FowardDraw 1
 Case "V"
   CDGen_SetDirection -30
   CDGen_FowardDraw 1.5
   CDGen_SetDirection 60
   CDGen_FowardDraw -1.5
 Case "W"
   CDGen_SetDirection -30
   CDGen_FowardDraw 1.5
   CDGen_SetDirection 60
   CDGen_FowardDraw -1
   CDGen_SetDirection -60
   CDGen_FowardDraw 1
   CDGen_SetDirection 60
   CDGen_FowardDraw -1.5
 Case "X"
   CDGen_SetDirection -45
   CDGen_FowardDraw 1.4
   CDGen_MoveToMiddle 0, 50
   CDGen_SetDirection 90
   CDGen_FowardDraw 0.7
   CDGen_MoveToMiddle 0, 50
   CDGen_FowardDraw -0.7
 Case "Y"
   CDGen_SetDirection -30
   CDGen_FowardDraw 0.5
   CDGen_SetDirection 60
   CDGen_FowardDraw -0.5
   CDGen_MoveToMiddle 0, 100
   CDGen_SetDirection -30
   CDGen_FowardDraw 0.5
 Case "Z"
   CDGen_SetDirection -90
   CDGen_FowardDraw 1
   CDGen_SetDirection -45
   CDGen_FowardDraw -1.4
   CDGen_SetDirection 45
   CDGen_FowardDraw 1
 End Select
 CDGen_Flush X0, Y0
End Sub

Sub CDGen_Blur()
 ' �ﲣ�ͪ��I�}�϶i��X�ƳB�z
 Dim i, j

 For j = 1 To PicHeight - 2
  For i = 1 To PicWidth - 2
   If Buf(j, i) = 0 Then
    If ((Buf(j, i - 1) Or Buf(j + 1, i)) And 1) <> 0 Then
     ' �p�G��e�I�O�ť��I�A�B�W�U���k�|���I�����@���I�O�����I�A�h���I���X�ƳB�z
     Buf(j, i) = 2
    End If
   End If
  Next
 Next
End Sub

Sub CDGen_NoisyLine()
 Dim i
 For i=1 To nNoisyLineCount
  CDGen_Line Rnd * PicWidth, Rnd * PicHeight, Rnd * PicWidth, Rnd * PicHeight
 Next
End Sub

Sub CDGen_NoisyDot()
 ' �ﲣ�ͪ��I�}�϶i�澸�I�B�z
 Dim i, j, NoisyDot, CurDot

 For j = 0 To PicHeight - 1
  For i = 0 To PicWidth - 1
   If Buf(j, i) <> 0 Then
    If Rnd < nColorNoisyDotOdds Then
     Buf(j, i) = 8
    Else
     Buf(j, i) = nSaturation
    End If
   Else
    If Rnd < nBlankNoisyDotOdds Then
     Buf(j, i) = nSaturation
    Else
     Buf(j, i) =8
    End If
   End If
  Next
 Next
End Sub

Sub CDGen()
 ' �ͦ��I�}�ϰ}�C
 Dim i, Ch, w, x, y

 DigtalStr = ""
 CDGen_Clear
 w = nImgWidth / nCharCount

 For i = 0 To nCharCount - 1
  nPixelWidth = w * (1 + (Rnd * 2 - 1) * nCharWidthRandom / 100)
  nPixelHeight = nImgHeight * (1 - Rnd * nCharHeightRandom / 100)
  x = nSpaceX + w * (i + (Rnd * 2 - 1) * nPositionXRandom / 100)
  y = nSpaceY + nImgHeight * (Rnd * 2 - 1) * nPositionYRandom / 100

  Ch = Mid(cCharSet, Int(Rnd * Len(cCharSet)) + 1, 1)
  DigtalStr = DigtalStr + Ch

  CDGen_Char Ch, x, y
 Next
 CDGen_Blur
 CDGen_NoisyLine
 CDGen_NoisyDot
End Sub

Function HSBToRGB(vH, vS, vB)
 ' �N�C��ȥ�HSB�ഫ��RGB
 Dim aRGB(3), RGB1st, RGB2nd, RGB3rd
 Dim nH, nS, nB
 Dim lH, nF, nP, nQ, nT

 vH = (vH Mod 360)
 If vS > 100 Then
  vS = 100
 ElseIf vS < 0 Then
  vS = 0
 End If
 If vB > 100 Then
  vB = 100
 ElseIf vB < 0 Then
  vB = 0
 End If
 If vS > 0 Then
  nH = vH / 60
  nS = vS / 100
  nB = vB / 100
  lH = Int(nH)
  nF = nH - lH
  nP = nB * (1 - nS)
  nQ = nB * (1 - nS * nF)
  nT = nB * (1 - nS * (1 - nF))
  Select Case lH
  Case 0
   aRGB(0) = nB * 255
   aRGB(1) = nT * 255
   aRGB(2) = nP * 255
  Case 1
   aRGB(0) = nQ * 255
   aRGB(1) = nB * 255
   aRGB(2) = nP * 255
  Case 2
   aRGB(0) = nP * 255
   aRGB(1) = nB * 255
   aRGB(2) = nT * 255
  Case 3
   aRGB(0) = nP * 255
   aRGB(1) = nQ * 255
   aRGB(2) = nB * 255
  Case 4
   aRGB(0) = nT * 255
   aRGB(1) = nP * 255
   aRGB(2) = nB * 255
  Case 5
   aRGB(0) = nB * 255
   aRGB(1) = nP * 255
   aRGB(2) = nQ * 255
  End Select
 Else
  aRGB(0) = (vB * 255) / 100
  aRGB(1) = aRGB(0)
  aRGB(2) = aRGB(0)
 End If
 HSBToRGB = ChrB(Round(aRGB(2), 0)) & ChrB(Round(aRGB(1), 0)) & ChrB(Round(aRGB(0), 0))
End Function

Sub CreatValidCode(pSN)
 Dim i, j, CurColorHue

 CDGen
 Session(pSN) = DigtalStr '�O���JSession

 Dim FileSize, PicDataSize
 PicDataSize = PicWidth * PicHeight * 3
 FileSize = PicDataSize + 54

 ' ��XBMP�ɸ�T�Y
 Response.BinaryWrite ChrB(66) & ChrB(77) & ChrB(FileSize Mod 256) & ChrB((FileSize \ 256) Mod 256) & ChrB((FileSize \ 256 \ 256) Mod 256) & ChrB(FileSize \ 256 \ 256 \ 256) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(54) & ChrB(0) & ChrB(0) & ChrB(0)

 ' ��XBMP�I�}�ϸ�T�Y
 Response.BinaryWrite ChrB(40) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(PicWidth Mod 256) & ChrB((PicWidth \ 256) Mod 256) & ChrB((PicWidth \ 256 \ 256) Mod 256) & ChrB(PicWidth \ 256 \ 256 \ 256) & ChrB(PicHeight Mod 256) & ChrB((PicHeight \ 256) Mod 256) & ChrB((PicHeight \ 256 \ 256) Mod 256) & ChrB(PicHeight \ 256 \ 256 \ 256) & ChrB(1) & ChrB(0) & ChrB(24) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(PicDataSize Mod 256) & ChrB((PicDataSize \ 256) Mod 256) & ChrB((PicDataSize \ 256 \ 256) Mod 256) & ChrB(PicDataSize \ 256 \ 256 \ 256) & ChrB(18) & ChrB(11) & ChrB(0) & ChrB(0) & ChrB(18) & ChrB(11) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0) & ChrB(0)

 ' �v�I��X�I�}�ϰ}�C
 If nColorHue = -1 Then
  CurColorHue = Int(Rnd * 360)
 ElseIf nColorHue <> -2 Then
  CurColorHue = nColorHue
 End If
 For j = 0 To PicHeight - 1
  For i = 0 To PicWidth - 1
   If nColorHue = -2 Then
    Response.BinaryWrite HSBToRGB(0, 0, 100 - Buf(PicHeight - 1 - j, i))
   Else
    Response.BinaryWrite HSBToRGB(CurColorHue, Buf(PicHeight - 1 - j, i), 100)
   End If
  Next
 Next
End Sub
%>