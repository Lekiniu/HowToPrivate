
    $(document).on("click", "#PhotoId", function (e) {
        var primaryValue = $(this).val();
    var artId = $('#articleId').val();
            if (confirm('Are you sure?')){
        $.ajax({
            type: 'POST',
            data: { 'primary': primaryValue, 'id': artId },
            url: /*'@Url.Action("DeleteImages", "Admin")',*/ "/Admin/DeleteImages/",
            context: this,
            success: function () {
                $('.img').remove();
                $("#pics").load(location.href + " #pics");
                $("#main-artpic").load(location.href + " #main-artpic");
                $("#deleteImageConfirm").css("display", "block");
                
            }
        });
    }
    return false;
});


//script for Main Image

 $(document).ready(function () {
        if (localStorage.selected) {
        $("#" + localStorage.selected).attr('checked', true);
    }
        $('.radioButton').click(function () {
        localStorage.setItem("selected", this.id);
    });
    });




    $(document).on("click", ".radioButton", function () {
        var primaryValue = $(this).val();
        var artId = $('#articleId').val();
                if (confirm('Are you sure?')){
        $.ajax({
            type: 'POST',
            data: { 'primary': primaryValue, 'id': artId },
            url: /*'@Url.Action("EditMainImage","Admin")',*/"/Admin/EditMainImage/",
            context: this,
            success: function () {
                $("#main-artpic").load(location.href + " #main-artpic");
            }
        });
    }
});



