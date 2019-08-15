
    $(document).ready(function () {
        $('#checkBoxAll').click(function () {
            if ($(this).is(":checked"))
                $('.checkBoxId').prop('checked', true);
            else
                $('.checkBoxId').prop('checked', false);
        });
    });
