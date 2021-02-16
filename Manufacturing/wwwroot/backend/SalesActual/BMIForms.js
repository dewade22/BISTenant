
let dates = new Date();
let month = dates.getMonth()+1;
let year = dates.getFullYear();
let day = dates.getDate();
let fullDT = month + '-' + day + '-' + year;
let baseurl = window.location.origin;


let bulan = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];


$(document).ready(function () {
    Days();
})

$('#search').click(function () {
    if ($('#datePicker').val() != null) {
        let parseDate = $('#datePicker').val().split('/');
        let myDate = parseDate[1] + '-' + parseDate[0] + '-' + parseDate[2]
        jQuery("#tableBMIMonth tbody").empty();
        Days(myDate);
    }
})
function Days(date = fullDT) { 
    let parseDate = date.split('-');
    let myDate = new Date(parseDate[2], parseDate[0] - 1, parseDate[1]);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        data: {
            DateTime: date
        },
        url:  baseurl+ '/BalimoonBMI/SalesActual/BMIDaysMonth',
        success: function (res) {
            $('#transactionDate').html(`${myDate.getDate()} ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#noDays').html(res.noDaysMonth);
            $('#totalDays').html(res.totalDayMonth);           
            $('#tglIni').html(`${myDate.getDate()}-${bulan[myDate.getMonth()]}`);
            $('#sdTglIni').html(`s/d : ${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
            $('#persenHari').html(`${Math.round(((res.noDaysMonth / res.totalDayMonth*100)+Number.EPSILON)*100)/100}%`)
            insertSalesActual(date, res);
        },
    });
};
function insertSalesActual(date, res) {
    $.ajax({
        type: 'GET',
        dataType: 'json',
        data: {
            DateTime: date
        },
        url: baseurl + '/BalimoonBMI/SalesActual/BMISalesAndTarget',
        success: function (data) {
            //console.log(data.salesActual.value)
            let result = data.salesActual.value
            for (let i = 0; i < result.length; i++){
                let isi = `<tr>
                                <td>${result[i].category}</td>
                                <td>${numberWithCommas(result[i].todayRevenue)}</td>
                                <td>${numberWithCommas(result[i].monthlyRevenue)}</td>
                                <td>${Math.round(((result[i].monthlyRevenue / result[i].monthlyRevenueBudget*100)+Number.EPSILON)*100)/100} %</td>
                                <td>${numberWithCommas(Math.round(result[i].monthlyRevenue/res.noDaysMonth))}</td>
                                <td>${numberWithCommas(result[i].monthlyRevenueBudget)}</td>
                                <td>${numberWithCommas(Math.round(result[i].monthlyRevenueBudget/res.totalDayMonth))}</td>
                                <td>${numberWithCommas(result[i].monthlyRevenueBudget-result[i].monthlyRevenue)}</td>
                                <td>${res.totalDayMonth - res.noDaysMonth == 0 ? numberWithCommas(result[i].monthlyRevenueBudget - result[i].monthlyRevenue) : numberWithCommas(Math.round((result[i].monthlyRevenueBudget - result[i].monthlyRevenue) / (res.totalDayMonth - res.noDaysMonth)))}</td>
                           </tr>`
                jQuery("#tableBMIMonth tbody").append(isi);
            }
            let pemisah = `<tr><td colspan="9"></td></tr>`
            jQuery("#tableBMIMonth tbody").append(pemisah);
        },
        done: function () {
            
        }
    })
};
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
