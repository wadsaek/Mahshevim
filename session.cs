<system.web>
    <sessionState timeout="40" mode="InProc" cookieless="UseCookies"/> //конфиги для использования данных сессии
</system.web>
//________________________________

//insert data
Session["variablename"] = variable //for example: Session["email"] = Request.Form["email"]

//get data
variable = Session["variablename"]