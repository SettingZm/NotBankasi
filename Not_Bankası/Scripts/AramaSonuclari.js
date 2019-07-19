   $(document).ready(function () {

        $("#secilenuniversiteID").change(function () {
            var donen = $("#secilenuniversiteID").val();
            $("#bolumler").empty();
            $.ajax({
                url: '/AnaSayfa/BolumSecme/' + donen,
                type: 'POST',
                dataType: 'json',
                success: function (data) {
                    $.each(data, function (i, item) {
                        $("#bolumler").append(
                            "<option>" + item + "</option>"
                        );
                    });
                }
            });
        });
    });