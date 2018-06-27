function getFeedBackByKey() {
    var searchFeedbackFor = document.forms["searchFeedbackForm"];

    var searchkey = searchFeedbackFor.elements["searchkey"].value;

    Ajax.post("/FeedBack/SearchByKey",
            { 'searchkey': searchkey },
            false,
            getFeedBackResponse)
}

function getFeedBackResponse(data) {
    var result = eval("(" + data + ")");
    
    if (result.state == "success") {
        var obj = result.content;

        var list = "<div class='cont_page_main_tab'>回复问题列表</div>";
        list += "<div><table width='100%' class='tbstyle1' cellpadding='5' cellspacing='1'>";
        list += "<tr><th style='width:80px;'>主题：</th><td>" + obj.title + "</td></tr>";
        list += "<tr><th>内容：</th><td>" + obj.body + "</td></tr>";
        list += "<tr><th>回复：</th><td>" + obj.reply + "</td></tr></table>";
        
        $("#searchresult").html(list);        
    }
    else {
        alert(result.content);
    }
}



//提交信息反馈信息
function submitFeedBack() {
    var submitFeedbackForm = document.forms["submitFeedbackForm"];

    var typeid = submitFeedbackForm.elements["typeid"].value;
    var linkman = submitFeedbackForm.elements["LinkMan"].value;
    var tag = submitFeedbackForm.elements["tag"].value;
    var tel = submitFeedbackForm.elements["Tel"].value;
    var mobile = submitFeedbackForm.elements["Tel"].value;
    var email = submitFeedbackForm.elements["Email"].value;
    var title = submitFeedbackForm.elements["Title"].value;
    var body = submitFeedbackForm.elements["Body"].value;
    var isout = submitFeedbackForm.elements["isOut"].checked ? 1 : 0;

    if (!verifySubmitFeedbackForm(typeid, linkman, title, body)) {
        return;
    }

    Ajax.post("/FeedBack/SubmitFeedBack",
            { 'typeid': typeid, 'linkman': linkman, 'tag': tag, 'tel': tel, 'mobile': mobile, 'email': email, 'title': title, 'body': body, 'isout': isout },
            false,
            submitFeedBackResponse)
}
//验证信息反馈
function verifySubmitFeedbackForm(typeid, linkman, title, body) {
    //if (typeid.length < 1) {
    //    alert("参数传递错误");
    //    return false;
    //}
    if (linkman.length < 1) {
        alert("联系人必须填写");
        return false;
    }
    if (title.length < 1) {
        alert("主题必须填写");
        return false;
    }
    if (body.length < 1) {
        alert("内容必须填写");
        return false;
    }
    return true;
}
//处理提交信息反馈的返回信息
function submitFeedBackResponse(data) {
    var result = eval("(" + data + ")");
    if (result.state != "success") {
        alert(result.content);
    }
    else {
        alert("您的信息已经提交成功，请耐心等待回复！\r\n您的查询码是：" + result.content + ",请牢记！");
    }
}