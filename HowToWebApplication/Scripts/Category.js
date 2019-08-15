
//not to submit form on enter click
$(document).ready(function () {
    $('#CategoryGroup').hide();
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
    
    //show new category form when pressed other option
    $('.ParentCategory').change(function () {
        if ($('.ParentCategory').val() !== '0'){
             $('#CategoryGroup').hide();
        } else {
           $('#CategoryGroup').show();
        }
    });


 //  input new category and update category form with input
    $('#AddButton').on("click", function (e) {
        var CategoryGroup = $('#CategoryInput');
        if (CategoryGroup.val() == '') {
            alert("please enter the new category ");
        }
        else {      
            var json = { "categoryName": CategoryGroup.val() };
           // CategoryGroup.val() = '';
                $.ajax({
                    async: true,
                    type: "POST",
                    url: "/Admin/AddNewCategory",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(json),
                    success: function (data) {
                        $('#ParentId').html('');
                        jQuery.each(data, function (i, val) {
                            $('<option>').val(val.Id).text(val.name).appendTo('#ParentId');
                        });
                        $('#CategoryGroup').hide();

                    },
                    error: function (request, status, error) {
                        alert(error + " " + status)
                    }
                });    
        }
    });
});