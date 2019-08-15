

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

$("#btnCreateAsset").on("click", function () {

    var url = $(this).data("url");

    $.get(url, function (data) {
        $('#createAssetContainer').html(data);

        $('#createAssetModal').modal('show');

    });

});

function CreateAssetSuccess(data) {

    if (data != "success") {
        $('#createAssetContainer').html(data);
        return;
       
    }
    $('#createAssetModal').modal('hide');
    $('#createAssetContainer').html("");
    //$("#table").load(location.href + " #table>*", "");
   
    setTimeout(function () {
        window.location.reload();
        updateDiv();                 
    }, 50);
}