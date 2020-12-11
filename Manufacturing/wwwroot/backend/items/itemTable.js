$(function () {
    var planningFilter = function () {
        $("#table-items").bootgrid({
            ajax: true,
            rowSelect: true,
            post: function () {
                return {
                    id: "b0df282a-0d67-40e5-8558-c9e93b7befed"
                };
            },
            ajaxSettings: {
                method: "GET",
                cache: false
            }, 
            url: "https://" + $("#path").val() + "/getItems",
        })
    }
    planningFilter();
});