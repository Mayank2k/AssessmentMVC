    $(function () {
        $("#checkAll").click(function () {
            $("input[name='recordsToDelete']").attr("checked", this.checked);

            $("input[name='recordsToDelete']").click(function () {
                if ($("input[name='recordsToDelete']").length == $("input[name='recordsToDelete']:checked").length) {
                    $("#checkAll").attr("checked", "checked");
                }
                else {
                    $("#checkAll").removeAttr("checked");
                }
            });

        });
        $("#btnSubmit").click(function () {
            var count = $("input[name='recordsToDelete']:checked").length;
            if (count == 0) {
                alert("No rows selected to delete");
                return false;
            }
            else {
                return confirm(count + " row(s) will be deleted");
            }
        });
    });
