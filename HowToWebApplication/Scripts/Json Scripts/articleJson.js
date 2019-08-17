

//$("#btnCreateArticle").on("click", function () {
//    var url = $(this).data("url");
//    $.get(url, function (data) {
//        $('#createArticleContainer').html(data);

//        $('#createArticleContainer').modal('show');
//    });
//});


//function CreateArticleSuccess(data) {
//    if (data != "success") {
//        $('#createArticleContainer').html(data);
//        return;
//    }
//    else {
//        $('#createArticleModal').modal('hide');
//        $('#createArticleContainer').html("");
//    }
//}

//$("#btnCreateAsset").on("click", function () {

//    var url = $(this).data("url");

//    $.get(url, function (data) {
//        $('#createAssetContainer').html(data);

//        $('#createAssetModal').modal('show');

//    });

//});

//function CreateAssetSuccess(data) {

//    if (data != "success") {
//        $('#createAssetContainer').html(data);
//        return;
       
//    }
//    $('#createAssetModal').modal('hide');
//    $('#createAssetContainer').html("");
  
   
//    setTimeout(function () {
//        window.location.reload();
//        updateDiv();                 
//    }, 50);
//}


//$('#btnArticleDetails').on("click", function () {

//    alert("ika");
//    var url = $(this).data("url");

//    $.get(url, function (data) {
//        $('#detailsAssetContainer').html(data);

//        $('#detailsAssetModal').modal('show');
//    });

//});

$(function () {
    $.ajaxSetup({ cache: false });
    $("a[data-modal]").on("click", function (e) {
        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                keyboard: true
            }, 'show');
            bindForm(this);
        });
        return false;
    });
});

function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $('#progress').show();
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#progress').hide();
                    location.reload();
                } else {
                    $('#progress').hide();
                    $('#myModalContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}