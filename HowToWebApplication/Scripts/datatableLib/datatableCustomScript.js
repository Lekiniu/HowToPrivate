
    $(document).ready(function () {
        $('#table').DataTable({
            "bStateSave": true,
            "fnStateSave": function (oSettings, oData) {
                localStorage.setItem('offersDataTables', JSON.stringify(oData));
            },
            "fnStateLoad": function (oSettings) {
                return JSON.parse(localStorage.getItem('offersDataTables'));
            },
            "columnDefs": [
                { "orderable": false, "targets": 0 }
            ]
        });
    });

