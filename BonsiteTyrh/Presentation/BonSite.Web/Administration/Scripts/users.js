//更新密码
function updatePassword() {
    var updatePasswordForm = document.forms["updatepasswordform"];

    //var v = updatePasswordForm.elements["v"].value;
    var v = "123";
    var oldpassword = updatePasswordForm.elements["oldpassword"].value;
    var newpassword = updatePasswordForm.elements["newpassword"].value;
    var newpassword1 = updatePasswordForm.elements["newpassword1"].value;

    if (oldpassword.length == 0) {
        alert("请输入密码");
        return;
    }
    if (newpassword != newpassword1) {
        alert("两次输入的密码不一样");
        return;
    }
    if (newpassword.length < 6)
    {
        alert("新密码长度至少为6位");
        return;
    }
    //if (verifyCode.length == 0) {
    //    alert("请输入验证码");
    //    return;
    //}

    Ajax.post("/admin/Account/SavePassword?v=" + v, { 'oldpassword': oldpassword, 'newpassword': newpassword, 'newpassword1': newpassword1 }, false, function (data) {
        var result = eval("(" + data + ")");
        if (result.state == "success") {
            alert("密码修改成功，请用新密码重新登录！！！" + result.content);
           // window.location.href = result.content;
            open('/Account/Login','_parent');
        }
        else {
            alert(result.content)
        }
    })
}



function Su() {
    var ppppp = $("#AdminGroupID option:selected").val();
    console.log("1"+ppppp)
    if (ppppp == "") {
       alert("所属角色不能为空")
    } 
}