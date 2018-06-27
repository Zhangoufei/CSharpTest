function submitVote() {
    var searchFeedbackFor = document.forms["voteForm"];

    var voteid = searchFeedbackFor.elements["voteid"].value;
    var results = searchFeedbackFor.elements["resultsid"];

    var resultsid = new Array();

    for (i = 0; i < results.length; i++)
    {
        if (results[i].checked)
            resultsid.push(results[i].value);
    }

    //alert(resultsid);

    Ajax.post("/Vote/SubmitVote",
            { 'voteid': voteid, 'resultid': resultsid },
            false,
            getVoteResponse)
}

function getVoteResponse(data) {
    var result = eval("(" + data + ")");
    
    if (result.state == "success") {
        alert("投票成功！");
    }
    else {
        alert(result.content);
    }
}


