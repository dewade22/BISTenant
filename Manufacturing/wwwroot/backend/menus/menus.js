let tenantAct = $("#tenantLayout").val().trim()
let baseUrls = window.location.origin
localStorage.setItem("thisAddress", baseUrls + '/' + tenantAct)
console.log(localStorage.getItem("thisAddress"));
    $(function () {
        $.ajax({
            type: "POST",
            url: baseUrls + "/" + tenantAct + "/Dashboard/GetDashboardsList",
            error: function (result) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: result
                })
            },
            success: function (result) {
                let menu="";
                $.each(result, function (key, value) {
                    menu += '<li><a href="' + baseUrls + '/' + tenantAct + '/Dashboard/Dashboard/' + value.id + '">' + value.name + '</a></li>';                  
                })
                $("#menuDash").html(menu)
            }

        })
    })