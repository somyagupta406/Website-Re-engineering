Imports System.Data.OleDb
Imports System.Net.Mail
Imports System.IO
Imports System.Data.SqlClient


Public Class WebForm1
    Inherits System.Web.UI.Page

    Dim strAccess As String = ""
    Dim strSQL As String = ""
    Dim strPath As String = String.Empty
    Dim strCitizenship As String = ""
    Dim strSoftware As String = ""
    Dim strHardware As String = ""
    Dim strMedical As String = ""
    Dim strMechanical As String = ""
    Dim strSales As String = ""
    Dim strEducation As String = ""
    Dim strGender As String = ""
    Dim strExperience As String = ""
    Dim strName As String = ""
    Dim strEmail As String = ""

    Dim strFileName As String = "Tickets.txt"
    Dim strTicketNumber As String = ""

    Dim strVehicleType As String = ""
    Dim strVehicleMake As String = ""
    Dim strlicensePlate As String = ""
    Dim strlicenseState As String = ""
    Dim strslashZero As String = ""
    Dim strblockNumber As String = ""
    Dim strstreet As String = ""
    Dim strticketDate As String = ""
    Dim strticketTime As String = ""
    Dim strmeterLocation As String = ""
    Dim strissuedBy As String = ""
    Dim strunknown1 As String = ""
    Dim strOriginalFine As String = ""
    Dim strFourteenDayFine As String = ""
    Dim strticketDay As String = ""
    Dim strunknown2 As String = ""
    Dim strviolationCode As String = ""
    Dim strviolationType As String = ""
    Dim strunknown3 As String = ""
    Dim strunknown4 As String = ""
    Dim strunknown5 As String = ""
    Dim strunknown6 As String = ""
    Dim strunknown7 As String = ""
    Dim strunknown8 As String = ""
    Dim strpostedLimit As String = ""


    Dim myConnection As OleDbConnection
    Dim myCommand As OleDbCommand
    Dim strEmailSubject As String = "Test-CSC470"

    Public Sub Connection()
        strPath = System.AppDomain.CurrentDomain.BaseDirectory()
        Session("path") = strPath

        strPath = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + strPath + "Database1.accdb"
        Session("access") = strPath


        strAccess = Session("access")
        myConnection = New OleDbConnection(strAccess)

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strPath As String = System.AppDomain.CurrentDomain.BaseDirectory()
        Dim strUserName As String = String.Empty


        strUserName = Environment.UserName
        'strUserName = ""
        Session("userName") = strUserName
        If Len(strUserName) = 0 Then
            Response.Redirect("Error.aspx")
        End If


        If Not Page.IsPostBack Then
            System.IO.File.WriteAllText(strPath + "output.txt", "")
            System.IO.File.WriteAllText(strPath + "outputSplit.txt", "")
            ObtainEvents()
            ObtainAnwers()
            ReadTextFile()
            ReadTextSplitMethod()
            DropTable("Tickets")
            CreateTable("Tickets")
            InsertSQL()

            If Session("gridView") = False Then
                gvTickets.Visible = False
            Else
                gvTickets.Visible = True
            End If
            LoadGridView()
        End If

        cbxHighDegree.Attributes.Add(“onclick”, “displayWindows()”)

    End Sub


    Public Sub btnSubmit_OnClick(sender As Object, e As EventArgs)
        'RdioButton Set 1-------

        If rbCitizenshipUs.Checked Then
            strCitizenship = "U.S Citizen"
        ElseIf rbCitizenshipOther.Checked Then
            strCitizenship = "Non U.S Citizen"
        Else
            strCitizenship = ""
        End If

        'Checkboxes-----
        If cbxSoftware.Checked Then
            strSoftware = "yes"
        Else
            strSoftware = "no"
        End If

        If cbxHardware.Checked Then
            strHardware = "yes"
        Else
            strHardware = "no"
        End If

        If cbxMedical.Checked Then
            strMedical = "yes"
        Else
            strMedical = "no"
        End If

        If cbxMechanical.Checked Then
            strMechanical = "yes"
        Else
            strMechanical = "no"
        End If

        If cbxSales.Checked Then
            strSales = "yes"
        Else
            strSales = "no"
        End If

        If cbxEducation.Checked Then
            strEducation = "yes"
        Else
            strEducation = "no"
        End If

        'RdioButton Set 2-------

        If rbGenderMale.Checked Then
            strGender = "Male"
        ElseIf rbGenderFemale.Checked Then
            strGender = "Female"
        Else
            strGender = ""
        End If

        'DropDownList------

        'strExperience = ddlYearsOfExp.SelectedValue

        ' Textbox 1--------------------------------------- 
        strName = txtName.Text


        ' Textbox --------------------------------------- 

        strEmail = txtEmail.Text

        '----  username and date time
        Dim strUserName As String = ""
        Dim dtChangeLast As DateTime = Now
        Session("userName") = Environment.UserName
        strUserName = Session("userName")

        ' insert ----------------------------- 
        Connection()

        strSQL = "insert into UserValues(Citizenship, Software, Hardware, Medical, Mechanical, Sales, Education, Gender, YearsOfExperience, Name, EmailId, UserName, DateTimeChangeLast) "
        strSQL += "values('" + strCitizenship + "', '" + strSoftware + "', '" + strHardware + "', '"
        strSQL += strMedical + "','" + strMechanical + "','" + strSales + "','" + strEducation + "', '"
        strSQL += strGender + "','" + strExperience + "','" + strName + "','" + strEmail + "','" + strUserName + "','" + dtChangeLast + "')"
        myCommand = New OleDbCommand(strSQL, myConnection)
        myCommand.Connection.Open()
        myCommand.ExecuteNonQuery()

        myCommand.Dispose()
        myConnection.Dispose()

        strEmailSubject = "Inserted Record Details"
        CreateEmail()
        btnClear_Click(Nothing, Nothing)
    End Sub
    Public Sub ObtainEvents()

        Dim myConnection As OleDbConnection
        Dim myCommand As OleDbCommand
        Dim myReader As OleDbDataReader

        Dim strEventName As String = ""
        Connection()
        myConnection = New OleDbConnection(strAccess)
        strSQL = "select * from Events order by EventName asc"
        myCommand = New OleDbCommand()
        myCommand.CommandText = strSQL
        myCommand.CommandType = CommandType.Text
        myCommand.Connection = myConnection
        myCommand.Connection.Open()

        myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        ddlYearsOfExp.Items.Clear()
        'lbxEvents.Items.Clear()

        ddlYearsOfExp.Items.Add(" - select an event - ")

        While (myReader.Read())
            strEventName = Trim("" + myReader("EventName"))
            ddlYearsOfExp.Items.Add(strEventName)
            '          lbxEvents.Items.Add(strEventName)
        End While

        myReader.Close()
        myCommand.Dispose()
        myConnection.Dispose()

    End Sub

    Public Sub ObtainAnwers()

        Dim myConnection As OleDbConnection
        Dim myCommand As OleDbCommand
        Dim myReader As OleDbDataReader
        Dim strUserName As String = ""
        Session("userName") = Environment.UserName
        strUserName = Session("userName")


        myConnection = New OleDbConnection(strAccess)
        strSQL = "select top 1 * from UserValues where UserName = '" + strUserName + "' order by DateTimeChangeLast desc"

        myCommand = New OleDbCommand()
        myCommand.CommandText = strSQL
        myCommand.CommandType = CommandType.Text
        myCommand.Connection = myConnection
        myCommand.Connection.Open()
        myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)

        While (myReader.Read())
            strCitizenship = Trim("" + myReader("Citizenship"))
            strSoftware = Trim("" + myReader("Software"))
            strHardware = Trim("" + myReader("Hardware"))
            strMedical = Trim("" + myReader("Medical"))
            strMechanical = Trim("" + myReader("Mechanical"))
            strSales = Trim("" + myReader("Sales"))
            strEducation = Trim("" + myReader("Education"))
            strGender = Trim("" + myReader("Gender"))
            strExperience = Trim("" + myReader("YearsOfExperience"))
            strName = Trim("" + myReader("Name"))
            strEmail = Trim("" + myReader("EmailId"))
        End While

        myReader.Close()
        myCommand.Dispose()
        myConnection.Dispose()


        ' Citizenship  radio buttons ------------------  
        If strCitizenship = "U.S Citizen" Then
            rbCitizenshipUs.Checked = True
        ElseIf strCitizenship = "Non U.S Citizen" Then
            rbCitizenshipOther.Checked = True
        Else
            rbCitizenshipUs.Checked = False
            rbCitizenshipOther.Checked = False
        End If

        ' Gender radio buttons ------------------  
        If strGender = "Male" Then
            rbGenderMale.Checked = True
        ElseIf strGender = "Female" Then
            rbGenderFemale.Checked = True
        Else
            rbGenderMale.Checked = False
            rbGenderFemale.Checked = False
        End If
        ' checkboxes -----------------------------  
        If strSoftware = "yes" Then
            cbxSoftware.Checked = True
        Else
            cbxSoftware.Checked = False
        End If

        If strHardware = "yes" Then
            cbxHardware.Checked = True
        Else
            cbxHardware.Checked = False
        End If

        If strMedical = "yes" Then
            cbxMedical.Checked = True
        Else
            cbxMedical.Checked = False
        End If

        If strMechanical = "yes" Then
            cbxMechanical.Checked = True
        Else
            cbxMechanical.Checked = False
        End If

        If strSales = "yes" Then
            cbxSales.Checked = True
        Else
            cbxSales.Checked = False
        End If

        If strEducation = "yes" Then
            cbxEducation.Checked = True
        Else
            cbxEducation.Checked = False
        End If

        ' ------Dropdown
        ddlYearsOfExp.SelectedValue = strExperience


        ' textbox1 ------------------------------  
        txtName.Text = strName

        ' textbox2 ------------------------------  
        txtEmail.Text = strEmail

    End Sub

    'Protected Sub ddlYearsOfExp_SelectedIndexChanged(sender As Object, e As EventArgs)
    '    strExperience = ddlYearsOfExp.SelectedValue
    'End Sub

    Protected Sub btnClear_Click(sender As Object, e As EventArgs)
        rbCitizenshipUs.Checked = False
        rbCitizenshipOther.Checked = False
        cbxSoftware.Checked = False
        cbxHardware.Checked = False
        cbxMedical.Checked = False
        cbxMechanical.Checked = False
        cbxSales.Checked = False
        cbxEducation.Checked = False
        rbGenderMale.Checked = False
        rbGenderFemale.Checked = False
        ddlYearsOfExp.ClearSelection()
        txtName.Text = String.Empty
        txtEmail.Text = String.Empty

    End Sub

    Protected Sub ddlYearsOfExp_SelectedIndexChanged(sender As Object, e As EventArgs)
        strExperience = ddlYearsOfExp.SelectedValue
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)

        'RdioButton Set 1-------

        If rbCitizenshipUs.Checked Then
            strCitizenship = "U.S Citizen"
        ElseIf rbCitizenshipOther.Checked Then
            strCitizenship = "Non U.S Citizen"
        Else
            strCitizenship = ""
        End If

        'Checkboxes-----
        If cbxSoftware.Checked Then
            strSoftware = "yes"
        Else
            strSoftware = "no"
        End If

        If cbxHardware.Checked Then
            strHardware = "yes"
        Else
            strHardware = "no"
        End If

        If cbxMedical.Checked Then
            strMedical = "yes"
        Else
            strMedical = "no"
        End If

        If cbxMechanical.Checked Then
            strMechanical = "yes"
        Else
            strMechanical = "no"
        End If

        If cbxSales.Checked Then
            strSales = "yes"
        Else
            strSales = "no"
        End If

        If cbxEducation.Checked Then
            strEducation = "yes"
        Else
            strEducation = "no"
        End If

        'RdioButton Set 2-------

        If rbGenderMale.Checked Then
            strGender = "Male"
        ElseIf rbGenderFemale.Checked Then
            strGender = "Female"
        Else
            strGender = ""
        End If

        'DropDownList------

        'strExperience = ddlYearsOfExp.SelectedValue

        ' Textbox 1--------------------------------------- 
        strName = txtName.Text


        ' Textbox --------------------------------------- 

        strEmail = txtEmail.Text

        '----  username and date time
        Dim strUserName As String = ""
        Dim dtChangeLast As DateTime = Now
        Session("userName") = Environment.UserName
        strUserName = Session("userName")

        ' insert ----------------------------- 
        Connection()

        strSQL = "update UserValues "
        strSQL += "set Citizenship = '" + strCitizenship + "', "
        strSQL += "Software = '" + strSoftware + "', "
        strSQL += "Hardware = '" + strHardware + "', "
        strSQL += "Medical = '" + strMedical + "', "
        strSQL += "Mechanical = '" + strMechanical + "', "
        strSQL += "Sales = '" + strSales + "', "
        strSQL += "Education = '" + strEducation + "', "
        strSQL += "Gender = '" + strGender + "', "
        strSQL += "YearsOfExperience = '" + strExperience + "', "
        strSQL += "Name = '" + strName + "', "
        strSQL += "EmailId = '" + strEmail + "', "
        strSQL += "DateTimeChangeLast = #" + dtChangeLast + "#"
        strSQL += "where UserName = '" + strUserName + "' "

        myCommand = New OleDbCommand(strSQL, myConnection)
        myCommand.Connection.Open()
        myCommand.ExecuteNonQuery()

        myCommand.Dispose()
        myConnection.Dispose()

        strEmailSubject = "Updated the record"
        CreateEmail()
        btnClear_Click(Nothing, Nothing)
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        'RdioButton Set 1-------

        If rbCitizenshipUs.Checked Then
            strCitizenship = "U.S Citizen"
        ElseIf rbCitizenshipOther.Checked Then
            strCitizenship = "Non U.S Citizen"
        Else
            strCitizenship = ""
        End If

        'Checkboxes-----
        If cbxSoftware.Checked Then
            strSoftware = "yes"
        Else
            strSoftware = "no"
        End If

        If cbxHardware.Checked Then
            strHardware = "yes"
        Else
            strHardware = "no"
        End If

        If cbxMedical.Checked Then
            strMedical = "yes"
        Else
            strMedical = "no"
        End If

        If cbxMechanical.Checked Then
            strMechanical = "yes"
        Else
            strMechanical = "no"
        End If

        If cbxSales.Checked Then
            strSales = "yes"
        Else
            strSales = "no"
        End If

        If cbxEducation.Checked Then
            strEducation = "yes"
        Else
            strEducation = "no"
        End If

        'RdioButton Set 2-------

        If rbGenderMale.Checked Then
            strGender = "Male"
        ElseIf rbGenderFemale.Checked Then
            strGender = "Female"
        Else
            strGender = ""
        End If

        'DropDownList------

        'strExperience = ddlYearsOfExp.SelectedValue

        ' Textbox 1--------------------------------------- 
        strName = txtName.Text


        ' Textbox --------------------------------------- 

        strEmail = txtEmail.Text

        '----  username and date time
        Dim strUserName As String = ""
        Dim dtChangeLast As DateTime = Now
        Session("userName") = Environment.UserName
        strUserName = Session("userName")

        ' insert ----------------------------- 
        Connection()


        strSQL = "delete from UserValues "
        strSQL += "where UserName = '" + strUserName + "' "

        myCommand = New OleDbCommand(strSQL, myConnection)
        myCommand.Connection.Open()
        myCommand.ExecuteNonQuery()

        myCommand.Dispose()
        myConnection.Dispose()

        strEmailSubject = "Deleted The Record"
        CreateEmail()
        btnClear_Click(Nothing, Nothing)
    End Sub
    Public Sub CreateEmail()
        Dim strMailFrom As String = "sgupt7@uis.edu"
        Dim strMailTo As String = "rjack01s@uis.edu"
        Dim strEmailName As String = "sgupt7@uis.edu"


        Dim mail As New MailMessage(strMailFrom, strMailTo)
        mail.To.Add(strEmailName)
        mail.Subject = strEmailSubject
        mail.IsBodyHtml = True

        mail.Body = ""
        mail.Body += strEmailSubject
        mail.Body += "<br /><br />"

        ' Citizen Radio Button
        mail.Body += "<span style='font-family:Verdana; font-size: 24px; color: Red;'>"

        mail.Body += "Select the citizen detail: "

        If rbCitizenshipUs.Checked Then
            mail.Body += "U.S Citizen"
        ElseIf rbCitizenshipOther.Checked Then
            mail.Body += "Non U.S Citizen"
        Else
            mail.Body += "No button was selected"
        End If
        mail.Body += "</span>"
        mail.Body += "<br />"

        ' Checkboxes
        mail.Body += "<span style='font-family:Verdana; font-size: 24px; color: Blue;'>"

        mail.Body += "Field Intrest: "

        If cbxSoftware.Checked Then
            mail.Body += "Software"
            mail.Body += "<br />"
        Else
            mail.Body += String.Empty
        End If

        If cbxHardware.Checked Then
            mail.Body += "Hardware"
            mail.Body += "<br />"
        Else
            mail.Body += String.Empty
        End If

        If cbxMedical.Checked Then
            mail.Body += "Medical"
            mail.Body += "<br />"
        Else
            mail.Body += String.Empty
        End If

        If cbxMechanical.Checked Then
            mail.Body += "Mechanical"
            mail.Body += "<br />"
        Else
            mail.Body += String.Empty
        End If

        If cbxSales.Checked Then
            mail.Body += "Sales"
            mail.Body += "<br />"
        Else
            mail.Body += String.Empty
        End If


        If cbxEducation.Checked Then
            mail.Body += "Education"
            mail.Body += "<br />"
        Else
            mail.Body += String.Empty
        End If
        mail.Body += "</span>"
        mail.Body += "<br />"


        ' Gender Radio Button
        mail.Body += "<span style='font-family:Verdana; font-size: 24px; color: Red;'>"
        mail.Body += "Gender: "


        If rbGenderMale.Checked Then
            mail.Body += "Male"
        ElseIf rbGenderFemale.Checked Then
            mail.Body += "Female"
        Else
            mail.Body += "No button was selected"
        End If
        mail.Body += "</span>"
        mail.Body += "<br />"

        'DropDownList------
        ' Gender Radio Button
        mail.Body += "<span style='font-family:Verdana; font-size: 24px; color: Green;'>"
        mail.Body += "Years Of Experience:"

        If strExperience.Length > 0 Then
            mail.Body += strExperience

        Else
            mail.Body += "Not selected"
        End If
        mail.Body += "</span>"
        mail.Body += "<br />"
        ' --- textbox1
        mail.Body += "<span style='font-family:Verdana; font-size: 24px; color: Black'>Name:&nbsp;"
        mail.Body += strName
        mail.Body += "</span>"
        mail.Body += "<br />"

        ' --- textbox2
        mail.Body += "<span style='font-family:Verdana; font-size: 24px; color: Black'>EmailId:&nbsp;"
        mail.Body += strEmail
        mail.Body += "</span>"
        mail.Body += "<br />"

        Dim mySmtp As New SmtpClient
        mySmtp.Host = "webmail.uis.edu"

        'mySmtp.Send(mail)

        lblEmail.Text = mail.Body

    End Sub ' CreateEmail 

    Public Sub ReadTextFile()

        Dim strPath As String = String.Empty
        strPath = Session("path")

        If strPath Is Nothing Then
            strPath = System.AppDomain.CurrentDomain.BaseDirectory()
        End If


        Try
            If File.Exists(strPath + strFileName) Then
                strPath = strPath
            End If
        Catch ex As Exception
            Dim e As String = ex.ToString
        End Try

        Dim sr As StreamReader = New StreamReader(strPath + strFileName)
        Dim strLine As String = String.Empty
        Dim strRestOfString As String = String.Empty
        Dim variableCounter As String = 1
        Dim strOutputLine As String = String.Empty
        Dim dblRunningBalance As Double = 0.0
        Dim found As Integer = 0


        Do
            strLine = sr.ReadLine()
            strRestOfString = strLine
            If (strRestOfString <> Nothing) Then
                variableCounter = 1
                Do While strRestOfString.Length > 0
                    found = InStr(strRestOfString, ";")

                    If found > 0 Then
                        Select Case variableCounter
                            Case 1
                                strTicketNumber = Left(strRestOfString, found - 1)
                            Case 2
                                strVehicleType = Left(strRestOfString, found - 1)
                            Case 3
                                strVehicleMake = Left(strRestOfString, found - 1)
                            Case 4
                                strlicensePlate = Left(strRestOfString, found - 1)
                            Case 5
                                strlicenseState = Left(strRestOfString, found - 1)
                            Case 6
                                strslashZero = Left(strRestOfString, found - 1)
                            Case 7
                                strblockNumber = Left(strRestOfString, found - 1)
                            Case 8
                                strstreet = Left(strRestOfString, found - 1)
                            Case 9
                                strticketDate = Left(strRestOfString, found - 1)
                            Case 10
                                strticketTime = Left(strRestOfString, found - 1)
                            Case 11
                                strmeterLocation = Left(strRestOfString, found - 1)
                            Case 12
                                strissuedBy = Left(strRestOfString, found - 1)
                            Case 13
                                strunknown1 = Left(strRestOfString, found - 1)
                            Case 14
                                strOriginalFine = Left(strRestOfString, found - 1)
                            Case 15
                                strFourteenDayFine = Left(strRestOfString, found - 1)
                            Case 16
                                strticketDay = Left(strRestOfString, found - 1)
                            Case 17
                                strunknown2 = Left(strRestOfString, found - 1)
                            Case 18
                                strviolationCode = Left(strRestOfString, found - 1)
                            Case 19
                                strviolationType = Left(strRestOfString, found - 1)
                            Case 20
                                strunknown3 = Left(strRestOfString, found - 1)
                            Case 21
                                strunknown4 = Left(strRestOfString, found - 1)
                            Case 22
                                strunknown5 = Left(strRestOfString, found - 1)
                            Case 23
                                strunknown6 = Left(strRestOfString, found - 1)
                            Case 24
                                strunknown7 = Left(strRestOfString, found - 1)
                            Case 25
                                strunknown8 = Left(strRestOfString, found - 1)
                            Case 26
                                strpostedLimit = Left(strRestOfString, found - 1)

                        End Select

                        strRestOfString = Mid(strRestOfString, found + 1, Len(strLine) - found)
                        variableCounter += 1
                    End If

                Loop


                dblRunningBalance += Convert.ToDouble(strOriginalFine)
                strOutputLine = strTicketNumber + " " + strOriginalFine + " " + dblRunningBalance.ToString()
                WriteToFile(strOutputLine, "output.txt")
            End If
        Loop Until strLine Is Nothing

        sr.Close()

    End Sub   ' end of ReadTextFile  

    Public Sub WriteToFile(ByVal strOutputLine As String, ByVal strOutputFileName As String)
        Dim strPath As String = Session("path")

        If strPath Is Nothing Then
            strPath = System.AppDomain.CurrentDomain.BaseDirectory()
        End If

        Using sw As StreamWriter = New StreamWriter(strPath + strOutputFileName, True)
            sw.WriteLine(strOutputLine)
        End Using
    End Sub

    Public Sub ReadTextSplitMethod()
        Dim strPath As String = String.Empty
        Dim strTxtfPath As String = String.Empty
        Dim strline As String = String.Empty
        Dim dblRunningBalance As Double = 0.0
        Dim strArrayline() As String
        Dim sr As StreamReader

        strPath = Session("path")
        If strPath.Length() <= 0 Then
            strPath = System.AppDomain.CurrentDomain.BaseDirectory()
        End If
        strTxtfPath = strPath + strFileName
        Try
            If File.Exists(strTxtfPath) Then
                sr = New StreamReader(strTxtfPath)
                Do
                    strline = sr.ReadLine()
                    strArrayline = Split(strline, ";")
                    If strArrayline.Length > 0 And strArrayline.Length <= 27 Then
                        strTicketNumber = strArrayline(0)
                        strVehicleType = strArrayline(1)
                        strVehicleMake = strArrayline(2)
                        strlicensePlate = strArrayline(3)
                        strlicenseState = strArrayline(4)
                        strslashZero = strArrayline(5)
                        strblockNumber = strArrayline(6)
                        strstreet = strArrayline(7)
                        strticketDate = strArrayline(8)
                        strticketTime = strArrayline(9)
                        strmeterLocation = strArrayline(10)
                        strissuedBy = strArrayline(11)
                        strunknown1 = strArrayline(12)
                        strOriginalFine = strArrayline(13)
                        strFourteenDayFine = strArrayline(14)
                        strticketDay = strArrayline(15)
                        strunknown2 = strArrayline(16)
                        strviolationCode = strArrayline(17)
                        strviolationType = strArrayline(18)
                        strunknown3 = strArrayline(19)
                        strunknown4 = strArrayline(20)
                        strunknown5 = strArrayline(21)
                        strunknown6 = strArrayline(22)
                        strunknown7 = strArrayline(23)
                        strunknown8 = strArrayline(24)
                        strpostedLimit = strArrayline(25)
                    End If
                    dblRunningBalance += Convert.ToDouble(strOriginalFine)
                    WriteToFile(strTicketNumber + " " + strOriginalFine + " " + dblRunningBalance.ToString(), "outputSplit.txt")
                Loop Until strline Is Nothing
                sr.Close()
            Else
            End If
        Catch e As Exception

        End Try
    End Sub

    Public Sub CreateTable(ByVal strTableName As String)

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        myConnection = New SqlConnection()
        myCommand = New SqlCommand()
        myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("mySQLConn").ConnectionString
        strSQL = String.Empty
        strSQL += "CREATE TABLE " + strTableName + "("
        strSQL += "TicketNumber varchar(50) not null, "
        strSQL += "VehicleType varchar(50) not null, "
        strSQL += "VehicleMake varchar(50) null, "
        strSQL += "LicensePlate varchar(50) null, "
        strSQL += "LicenseState varchar(50) null, "
        strSQL += "SlashZero varchar(50) null, "
        strSQL += "BlockNumber varchar(50) null, "
        strSQL += "Street varchar(50) null, "
        strSQL += "TicketDate varchar(50) null, "
        strSQL += "TicketTime varchar(50) null, "
        strSQL += "MeterLocation varchar(50) null, "
        strSQL += "IssuedBy varchar(50) null, "
        strSQL += "Unknown1 varchar(50) null, "
        strSQL += "OriginalFine varchar(50) null, "
        strSQL += "FourteenDayFine varchar(50) null, "
        strSQL += "TicketDay varchar(50) null, "
        strSQL += "Unknown2 varchar(50) null, "
        strSQL += "ViolationCode varchar(50) null, "
        strSQL += "ViolationType varchar(50) null, "
        strSQL += "Unknown3 varchar(50) null, "
        strSQL += "Unknown4 varchar(50) null, "
        strSQL += "Unknown5 varchar(50) null, "
        strSQL += "Unknown6 varchar(50) null, "
        strSQL += "Unknown7 varchar(50) null, "
        strSQL += "Unknown8 varchar(50) null, "
        strSQL += "PostedLimit varchar(50) null, "
        strSQL += "primary key(TicketNumber))"


        myCommand.CommandText = strSQL
        myCommand.CommandType = CommandType.Text
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
        myCommand.Dispose()
        myConnection.Dispose()


    End Sub


    Public Sub DropTable(ByVal strTableName As String)

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        myConnection = New SqlConnection()
        myCommand = New SqlCommand()
        myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("mySQLConn").ConnectionString
        '  Dim tableExist As Integer = TableSectionStyle
        'If Exists(Select * From Tickets where) Then

        'End If
        'If Table Then
        strSQL = String.Empty
        strSQL += "Drop TABLE " + strTableName
        myCommand.CommandText = strSQL
        myCommand.CommandType = CommandType.Text
        myCommand.Connection = myConnection
        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
        myCommand.Dispose()
        myConnection.Dispose()
    End Sub


    Public Sub InsertSQL()

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        myConnection = New SqlConnection()
        myCommand = New SqlCommand()
        Dim strPath As String = String.Empty
        Dim strTxtfPath As String = String.Empty
        Dim strline As String = String.Empty
        '  Dim dblRunningBalance As Double = 0.0
        Dim strArrayline() As String
        Dim sr As StreamReader
        Dim strsql As String = String.Empty
        strPath = Session("path")
        If strPath.Length() <= 0 Then
            strPath = System.AppDomain.CurrentDomain.BaseDirectory()
        End If
        strTxtfPath = strPath + strFileName
        Try
            If File.Exists(strTxtfPath) Then
                sr = New StreamReader(strTxtfPath)
                Do
                    strline = sr.ReadLine()
                    strArrayline = Split(strline, ";")
                    If strArrayline.Length > 0 And strArrayline.Length <= 27 Then
                        strTicketNumber = strArrayline(0)
                        strVehicleType = strArrayline(1)
                        strVehicleMake = strArrayline(2)
                        strlicensePlate = strArrayline(3)
                        strlicenseState = strArrayline(4)
                        strslashZero = strArrayline(5)
                        strblockNumber = strArrayline(6)
                        strstreet = strArrayline(7)
                        strticketDate = strArrayline(8)
                        strticketTime = strArrayline(9)
                        strmeterLocation = strArrayline(10)
                        strissuedBy = strArrayline(11)
                        strunknown1 = strArrayline(12)
                        strOriginalFine = strArrayline(13)
                        strFourteenDayFine = strArrayline(14)
                        strticketDay = strArrayline(15)
                        strunknown2 = strArrayline(16)
                        strviolationCode = strArrayline(17)
                        strviolationType = strArrayline(18)
                        strunknown3 = strArrayline(19)
                        strunknown4 = strArrayline(20)
                        strunknown5 = strArrayline(21)
                        strunknown6 = strArrayline(22)
                        strunknown7 = strArrayline(23)
                        strunknown8 = strArrayline(24)
                        strpostedLimit = strArrayline(25)
                    End If
                    strsql = "Insert Into Tickets(TicketNumber,VehicleType,VehicleMake,LicensePlate,LicenseState,"
                    strsql += "SlashZero,BlockNumber,Street,TicketDate,TicketTime,MeterLocation,IssuedBy,"
                    strsql += "Unknown1,OriginalFine,FourteenDayFine,TicketDay,Unknown2,ViolationCode,"
                    strsql += "ViolationType,Unknown3,Unknown4,Unknown5,Unknown6,Unknown7,Unknown8,PostedLimit)"
                    strsql += "values ('" + strTicketNumber + "',"
                    strsql += " '" + strVehicleType + "',"
                    strsql += " '" + strVehicleMake + "',"
                    strsql += " '" + strlicensePlate + "',"
                    strsql += " '" + strlicenseState + "',"
                    strsql += " '" + strslashZero + "',"
                    strsql += " '" + strblockNumber + "',"
                    strsql += " '" + strstreet + "',"
                    strsql += " '" + strticketDate + "',"
                    strsql += " '" + strticketTime + "',"
                    strsql += " '" + strmeterLocation + "',"
                    strsql += " '" + strissuedBy + "',"
                    strsql += " '" + strunknown1 + "',"
                    strsql += " '" + strOriginalFine + "',"
                    strsql += " '" + strFourteenDayFine + "',"
                    strsql += " '" + strticketDay + "',"
                    strsql += " '" + strunknown2 + "',"
                    strsql += " '" + strviolationCode + "',"
                    strsql += " '" + strviolationType + "',"
                    strsql += " '" + strunknown3 + "',"
                    strsql += " '" + strunknown4 + "',"
                    strsql += " '" + strunknown5 + "',"
                    strsql += " '" + strunknown6 + "',"
                    strsql += " '" + strunknown7 + "',"
                    strsql += " '" + strunknown8 + "',"
                    strsql += " '" + strpostedLimit + "')"
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("mySQLConn").ConnectionString

                    myCommand.CommandText = strsql
                    myCommand.CommandType = CommandType.Text
                    myCommand.Connection = myConnection
                    myConnection.Open()
                    myCommand.ExecuteNonQuery()
                    myConnection.Close()
                    myCommand.Dispose()
                    myConnection.Dispose()
                Loop Until strline Is Nothing
                sr.Close()
            Else
            End If
        Catch e As Exception

        End Try
    End Sub

    Public Sub LoadGridView()

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim myDataSet As DataSet = New DataSet()
        myConnection = New SqlConnection()
        myCommand = New SqlCommand()
        myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("mySQLConn").ConnectionString

        strSQL = "select * from Tickets"

        myCommand.CommandText = strSQL
        myCommand.CommandType = CommandType.Text
        myCommand.Connection = myConnection
        myConnection.Open()

        Dim ad As New SqlDataAdapter(myCommand)
        ad.Fill(myDataSet)
        gvTickets.DataSource = myDataSet
        gvTickets.DataBind()

        myConnection.Close()
        myCommand.Dispose()
        myConnection.Dispose()

    End Sub  ' LoadGridView 

    Protected Sub gvTickets_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvTickets.SelectedIndexChanged
        Dim strSelected As String = String.Empty
        Dim row As GridViewRow = gvTickets.SelectedRow()
        strSelected += "TicketNumber Selected is = " + row.Cells(1).Text
        lblGridView.Text = strSelected
        gvTickets.Visible = False

        Session("gridView") = "False"

    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        Dim table As DataTable = Me.GetData()
        table.DefaultView.Sort = sortExpression & direction
        gvTickets.DataSource = table
        gvTickets.DataBind()
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property
    Public Function GetData() As DataTable
        Dim table As New DataTable()

        Dim myConnection As SqlConnection
        'Dim myCommand As SqlCommand

        myConnection = New SqlConnection()
        ' myCommand = New SqlCommand()
        myConnection.ConnectionString = ConfigurationManager.ConnectionStrings("mySQLConn").ConnectionString

        strSQL = "select * from Tickets"

        Dim ad As New SqlDataAdapter(strSQL, myConnection)
        ad.Fill(table)
        ' myConnection.Close()
        'myCommand.Dispose()
        'myConnection.Dispose()
        Return table

    End Function

    Protected Sub OnPageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvTickets.PageIndex = e.NewPageIndex
        'Me.gvTickets.DataBind()
        LoadGridView()

    End Sub
End Class