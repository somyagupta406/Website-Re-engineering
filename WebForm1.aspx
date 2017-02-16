<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="WebForm1.aspx.vb" Inherits="WebApplication1.WebForm1" MaintainScrollPositionOnPostBack="true" EnableSessionState="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #GridView {
            width: 699px;
            margin-left: 314px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="GridView">
    <span>
        <asp:GridView ID="gvTickets" runat="server"
            AutoGenerateColumns="False"
            AutoGenerateSelectButton="true"
            BackColor="#DEBA84" 
            BorderColor="#DEBA84"
            BorderStyle="None" 
            BorderWidth="1px"
            CellPadding="3" 
            CellSpacing="2" 
            style="margin-left: 0px"
            SelectedRowStyle-BackColor="#738A9C" 
            Font-Bold="True" 
            ForeColor="White"
            Caption="Tickets GridView"
            CaptionAlign="Top"
            GridLines="None"
            AllowSorting="true"
            Allowpaging="true"
            onsorting="SortRecords"
            OnPageIndexChanging="OnPageIndexChanging"
            pagesize="10">

           <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510"/>
            <%--<SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />--%>
            


            <PagerSettings Mode="Numeric"
                Position="Bottom"
                PageButtonCount="10" />

            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />

            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <Columns>
                <asp:BoundField DataField="ticketNumber" sortExpression="TicketNumber" HeaderText="TicketNumber" HeaderStyle-Width="60px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" >
<HeaderStyle Width="60px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="licensePlate" sortExpression="LicensePlate" HeaderText="LicensePlate" HeaderStyle-Width="60px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" >
<HeaderStyle Width="60px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="vehicleMake" sortExpression="VehicleMake" HeaderText="VehicleMake" HeaderStyle-Width="60px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" >
<HeaderStyle Width="60px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="TicketDate" sortExpression="TicketDate" HeaderText="TicketDate" HeaderStyle-Width="60px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" >
<HeaderStyle Width="60px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("vehicleType")%>' />
                        <asp:Label ID="Label3" runat="server" Text=" / " />
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("vehicleMake")%>' />
                        <br />
                        <asp:Label ID="Label11" runat="server" Text='<%# Eval("blockNumber")%>' />
                        <asp:Label ID="Label12" runat="server" Text=" " />
                        <asp:Label ID="Label9" runat="server" Text='<%# Eval("street")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
        <asp:Label ID="lblGridView" runat="server"></asp:Label>
    </span>
    </div>
    <div class="adRow">
        advertising area 
      <div style="float: right; font-family: Arial; font-size: 18px; text-align: center; margin-right: 5px;">
          <a href="#" onclick="toggle_visibility('rowExpanded');" style="text-decoration: none; color: white;">1000 of Jobs Available!! Apply Now
                <div style="font-size: 36px; padding: 0px; margin: 0px; color: White; text-decoration: none;">
                    <asp:Label ID="lblPlus" runat="server" Text="+" />

                </div>
          </a>
      </div>
    </div>

    <div id="rowExpanded" class="adRowExpanded">
        <div style="align-items: center">
            <div>
                
                Citizenship:
                     <asp:RadioButton ID="rbCitizenshipUs" runat="server" GroupName="Citizenship" Text="U.S" />
                <asp:RadioButton ID="rbCitizenshipOther" runat="server" GroupName="Citizenship" Text="Other Country" />
                <asp:Label ID="lblEmail" runat="server" Text="" CssClass="label" ></asp:Label>
            </div>


            <div>
                <asp:Label ID="Label1" runat="server" Text="Job Field:" Font-Bold="true" Font-Size="18px" />
                <br />
                <asp:CheckBox ID="cbxSoftware" runat="server" Text="Software"/><br />
                <asp:CheckBox ID="cbxHardware" runat="server" Text="Hardware" /><br />
                <asp:CheckBox ID="cbxMedical" runat="server" Text="Medical" /><br />
                <asp:CheckBox ID="cbxMechanical" runat="server" Text="Mechanical" /><br />
                <asp:CheckBox ID="cbxSales" runat="server" Text="Sales" /><br />
                <asp:CheckBox ID="cbxEducation" runat="server" Text="Education" /><br />
            </div>
            <div>
                Gender:
                       <asp:RadioButton ID="rbGenderMale" runat="server" GroupName="Gender" Text="Male" />
                <asp:RadioButton ID="rbGenderFemale" runat="server" GroupName="Gender" Text="Female" /><br />

            </div>
            <div>
                <b>Experience:</b>
                <asp:DropDownList ID="ddlYearsOfExp" runat="server"  OnSelectedIndexChanged="ddlYearsOfExp_SelectedIndexChanged" >
                  <%--<asp:ListItem>--Select Years of Experience--</asp:ListItem>
                        <asp:ListItem>0-2years</asp:ListItem>
                        <asp:ListItem>2-4years</asp:ListItem>
                        <asp:ListItem>4-6years</asp:ListItem>
                        <asp:ListItem>6-8years</asp:ListItem>
                        <asp:ListItem>8years+</asp:ListItem>--%>
                          </asp:DropDownList><br />
                <br />
            </div>
            <div>
                <b>Enter Your Name:</b>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
                <b>Enter Your Email ID:</b>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
            </div>

            
                <asp:CheckBox ID="cbxHighDegree" runat="server" Text="Highest Degree Obtained"  /> <br/> 
                <div id="divHighDegree" style="margin-left: 50px;display:none" >
                        <asp:DropDownList ID="ddlHighDegree" runat="server"> 
                          <asp:ListItem>Select Highest Degree Obtained</asp:ListItem> 
                             <asp:ListItem>Masters in Mechnical Engineering</asp:ListItem> 
                             <asp:ListItem>MBA in Marketing</asp:ListItem> 
                             <asp:ListItem>Bachelors of Engineering in Computer Science</asp:ListItem> 
                             <asp:ListItem>Bachelors of Engineering in Mechnical Engineering</asp:ListItem> 
                             <asp:ListItem>M.D in Dental</asp:ListItem> 
                             <asp:ListItem>M.B.B.S</asp:ListItem> 
                             <asp:ListItem>M.D in Neurology</asp:ListItem> 
                          <asp:ListItem>Masters in Computer Science</asp:ListItem>     
            </asp:DropDownList> <br/><br/>
            </div> 
            <asp:Button ID="btnSubmit" runat="server" Text="Insert" OnClick="btnSubmit_OnClick" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"/>
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"/>
            
        </div>
    </div>

    <div>
        <img src="Images/Hero.PNG" width="1400" height="350" />

    </div>
    

 


    <div class="maps">

        <span class="country-order">ORDER FOOD IN YOUR COUNTRY:</span>
        <a href="#">
            <img src="Images/indiaflag.png" /></a>
        <a href="#">
            <img src="Images/pakistanflag.png" /></a>
        <a href="#">
            <img src="Images/singaporeflag.png" /></a>
        <a href="#">
            <img src="Images/malaysiaflag.png" /></a>
        <a href="#">
            <img src="Images/bangladeshflag.png" /></a>
        <a href="#">
            <img src="Images/indonesiaflag.png" /></a>
        <a href="#">
            <img src="Images/thailandflag.png" /></a>
        <a href="#">
            <img src="Images/hongkongflag.png" /></a>
        <a href="#">
            <img src="Images/vietnamflag.png" /></a>
        <a href="#">
            <img src="Images/taiwanflag.png" /></a>
        <button type="button" class="button-more">More Countries</button>

    </div>

    <hr />
    <ul class="order-steps">
        <li class="step one  text-center">
            <div>
                <div class="title">1. Search</div>
                <div class="description">Find restaurants that deliver<br />
                    to you by entering your address</div>
                <p></p>
            </div>
        </li>
        <li class="step two  text-center">
            <div>
                <div class="title">2. Choose</div>
                <div class="description">Browse hundreds of menus<br />
                    to find the food you like</div>
                <p></p>
            </div>
        </li>
        <li class="step three  text-center">
            <div>
                <div class="title">3. Pay</div>
                <div class="description">Pay fast &amp; secure online or
                    <br />
                    on delivery</div>
                <p></p>
            </div>
        </li>
        <li class="step four  text-center">
            <div>
                <div class="title">4. Enjoy</div>
                <div class="description">Food is prepared &amp; delivered
                    <br>
                    to your door</div>
                <p></p>
            </div>
        </li>
    </ul>

    <!--  <div class="homepagesteps">
       <div class="row">
        <ul class="order-stepsurl">
            <li class="order-stepsurl">
                <div>1.Search</div>
                <div>Find restaurants that deliver<br />
            to you by entering your address</div>

            </li>
        </ul>
           </div>
      
    </div>-->




    <section>
        <img src="Images/devices.png" class="devices" />
        <aside>
            <h3 class="go">Order on the go</h3>
            Tap, choose and enjoy from many restaurants in your neighbourhood.<br />
            And all this for FREE!
         <nav class="list1">
             <ul>
                 <li>
                     <img id="app1" src="Images/ios-applogo.png" width="122" height="38" />
                 </li>
                 <li>
                     <img src="Images/Android_applogo.png" width="122" height="38" />
                 </li>
                 <li>
                     <img src="Images/win_app_badge.png" width="122" height="38" />
                 </li>
             </ul>
         </nav>
        </aside>
    </section>


    <div style="width: 100%; height: 325px; padding: 0 0 0 66px; margin-bottom: -12px;">
        <div class="divleft">
            <h3 class="latestnews">Join us at foodpanda</h3>
            <br />
            <p class="carrer">
                We are a highly motivated team, aspiring to become the global synonym for online food ordering<br />
                <br />

                <a class="transparentbutton" style="margin-top: 5px;" href="#">CAREERS</a>
            </p>
        </div>
        <div class="divright">
            <h3 class="latestnews">Latest press releases</h3>
            <br />

            <h5 class="febnews">February 5, 2016<br />
            </h5>
            <p class="febnewspara">foodpanda group sells Brazilian and Mexican Business to JustEat<br />
            </p>

            <h5 class="febnews">November 18, 2015<br />
            </h5>
            <p class="febnewspara">foodpanda group launches separate product for corporate clients<br />
            </p>

            <h5 class="febnews">May 1, 2015<br />
            </h5>
            <p class="febnewspara">foodpanda raises additional USD 100 Million in funding led by Goldman Sachs<br />
                <br />
            </p>
            <!--  <p class="buttpresspara"><input type="button" value="PRESS" class="buttonpress" /></p>-->
            <p>
                <a class="transparentbutton" style="margin-top: 5px;" href="#">Press</a>
            </p>
        </div>

    </div>

    <section class="imagefeature">

        <p style="text-transform: uppercase; color: #9f9f9f; margin: 2px 0px -45px; font-weight: 800;">Featured on:</p>
        <br />
        <p>
            <a href="#">
                <img src="Images/newyorktimes.png" class="imagefeature-img" />
            </a>
            <a href="#">
                <img src="Images/techcrunch.png" class="imagefeature-img" />
            </a>
            <a href="#">
                <img src="Images/technisia.png" class="imagefeature-img" />
            </a>
            <a href="#">
                <img src="Images/TNW.png" class="imagefeature-img" style="width: 70px; height: 60px" />
            </a>
        </p>
    </section>


</asp:Content>
