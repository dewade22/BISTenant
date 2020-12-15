var dates = new Date();
const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"];
var month = monthNames[dates.getMonth()];
var year = dates.getFullYear();
$("#tbl_stock_Bulan").html(month + " - " + year);
let tenant = $('#tenantActive').val().trim();
let alamat = '/api/Dashboard/spiritWHFG?tenant=' +tenant;
//Get Spirit
(function Spirit() {
    $.ajax({
        type: 'GET',
        url:alamat,
        success: function (res) {
            if (tenant === 'Balimoon') {
                console.log("Balimoon")
            } else {
                console.log("NON Balimoon")
            }
        }
    })
    
})();