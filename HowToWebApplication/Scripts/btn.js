<script>
    var retrievedObject = localStorage.getItem('classChange');
    if (retrievedObject) {
        $('.tick').addClass(retrievedObject)
    }
    var retrievedImage = localStorage.getItem('imageClassChange');
    if (retrievedImage) {
        $('#imgId').addClass(retrievedImage)
    }
    $(".tick").click(function () {
        if ($(this).hasClass("btn-light")) {

        

        $(this).removeClass('btn-light').addClass('btn-success');
    localStorage.setItem('classChange', 'btn-success');
}
        else {
        $(this).removeClass('btn-success').addClass('btn-light');
    localStorage.setItem('classChange', 'btn-light');
}

//console.log(localStorage.getItem('imageClassChange'));
        if ($('#imgId').hasClass('imgClass')) {
        $('#imgId').removeClass('imgClass').addClass('mainImgClass');
    localStorage.setItem('imageClassChange', 'mainImgClass');
        } else {
        $('#imgId').removeClass('mainImgClass').addClass('imgClass');
    localStorage.setItem('imageClassChange', 'imgClass');
}

setMainImg();

return false;
});
</script>

    <script>
        var setMainImg = function () {
        var MainImglink = $('.main-img').map(function () {
            return $('.mainImgClass').attr('src');
    }).get();

    //$('main-img').attr('src', MainImglink[0]);
    console.log(MainImglink[0]);
    return MainImglink[0];
}
</script>