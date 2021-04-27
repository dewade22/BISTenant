let dates = new Date();
let month = dates.getMonth() + 1;
let year = dates.getFullYear();
let day = dates.getDate();
let fullDT = month + '-' + day + '-' + year;
let baseurl = window.location.origin;

let bulan = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];


$(document).ready(function () {
    SBToday();
    $('#tgl-transaksi').html(`${bulan[month - 1]}, ${day} - ${year}`)
    
})

$('#search').click(function () {
    if ($('#datePicker').val() != null) {
        let parseDate = $('#datePicker').val().split('/')
        let myDate = parseDate[1] + '-' + parseDate[0] + '-' + parseDate[2]
        let bln = parseInt(parseDate[1])
        $('#tgl-transaksi').html(`${bulan[bln-1]}, ${parseDate[0]} - ${parseDate[2]}`)
        jQuery('#tabel-SalesBoard tbody').empty()
        jQuery('#tabel-SalesBoard thead').empty()
        jQuery('#tabel-SalesBoard thead').append(`<tr><th colspan="2" class="headcol" style="width:300px">Produk</th></tr>`)
        SBToday(myDate)
    }
})


function SBToday(date = fullDT){
    let parseDate = date.split('-');
    let myDate = new Date(parseDate[2], parseDate[0] - 1, parseDate[1]);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        data: {
            DateTime: date
        },
        url: baseurl + '/BalimoonBMI/SalesActual/SBBMI',
        success: function (result) {
            $('#No-Days').html(result.hasil[0].daysNo)
            $('#total-Days').html(result.hasil[0].daysMonth)
            isiThead(result)
            isiBody(result)
            customScrolling() 
        }
    })
}

function isiThead(result) {
    for (let i = 0; i < result.sales; i++) {
        let thead = `<th style="width:400px" colspan=3>${result.hasil[i].salesPerson}</th>`
        jQuery("#tabel-SalesBoard thead tr").append(thead)
    }   
}

function isiBody(result) {
    for (let a = 0; a < result.hasil.length; a++) {
        jQuery("#tabel-SalesBoard tbody").append(`<tr><td rowspan="9" class="headcol">${result.hasil[a].itemCategory}</td></tr>`)
        isiHeaderBody(result, a)
        a = (a+result.sales)-1
    }
    
}

function isiHeaderBody(result, loop) {
    jQuery("#tabel-SalesBoard tbody tr:last-child").append(`<td></td>`)
    for (let a = 0; a < result.sales; a++) {
        let tables = `<td>Target</td>
                      <td>Achieved</td>
                      <td>% Diff</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiLiters(result, loop)
}

function isiLiters(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Liters</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${result.hasil[i].litersBudget.toFixed()}</td>
                      <td>${result.hasil[i].litersMonth}</td>
                      <td>${result.hasil[i].litersBudget == 0 ? `0` : `${Math.round(((result.hasil[i].litersMonth / result.hasil[i].litersBudget * 100)+Number.EPSILON)*100)/100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiQuantity(result, loop)
}

function isiQuantity(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Quantity</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${result.hasil[i].qtyBudget}</td>
                      <td>${result.hasil[i].qtyMonth}</td>
                      <td>${result.hasil[i].qtyBudget == 0 ? `0` : `${Math.round(((result.hasil[i].qtyMonth / result.hasil[i].qtyBudget * 100) + Number.EPSILON) * 100) / 100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiCases(result, loop)
}

function isiCases(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Cases</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${(result.hasil[i].litersBudget/8.4).toFixed(2)}</td>
                      <td>${(result.hasil[i].litersMonth/8.4).toFixed(2)}</td>
                      <td>${(result.hasil[i].litersBudget / 8.4) == 0 ? `0` : `${Math.round((((result.hasil[i].litersMonth / 8.4) / (result.hasil[i].litersBudget/8.4) * 100) + Number.EPSILON) * 100) / 100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiRevenue(result, loop)
}

function isiRevenue(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Revenue</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${result.hasil[i].revenueBudget.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      <td>${result.hasil[i].revenueMonth.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                      <td>${result.hasil[i].revenueBudget == 0 ? `0` : `${Math.round(((result.hasil[i].revenueMonth / result.hasil[i].revenueBudget * 100) + Number.EPSILON) * 100) / 100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiDailyLiters(result, loop)
}

function isiDailyLiters(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Daily Liters</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${(result.hasil[i].litersBudget / result.hasil[i].daysMonth).toFixed(2)}</td>
                      <td>${result.hasil[i].litersDay}</td>
                      <td>${result.hasil[i].litersBudget == 0 ? `0` : `${Math.round(((result.hasil[i].litersDay / (result.hasil[i].litersBudget / result.hasil[i].daysMonth) * 100) + Number.EPSILON) * 100) / 100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiDailyQty(result, loop)
}

function isiDailyQty(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Daily Qty</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${(result.hasil[i].qtyBudget / result.hasil[i].daysMonth).toFixed(2)}</td>
                      <td>${result.hasil[i].qtyDay}</td>
                      <td>${result.hasil[i].qtyBudget == 0 ? `0` : `${Math.round(((result.hasil[i].qtyDay / (result.hasil[i].qtyBudget / result.hasil[i].daysMonth) * 100) + Number.EPSILON) * 100) / 100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiDailyCases(result, loop)
}

function isiDailyCases(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Daily Cases</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${((result.hasil[i].litersBudget / result.hasil[i].daysMonth) / 8.4).toFixed(2)}</td>
                      <td>${(result.hasil[i].litersDay / 8.4).toFixed(2)}</td>
                      <td>${(result.hasil[i].litersBudget / 8.4) == 0 ? `0` : `${Math.round((((result.hasil[i].litersDay / 8.4) / ((result.hasil[i].litersBudget / result.hasil[i].daysMonth) / 8.4) * 100) + Number.EPSILON) * 100) / 100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
    isiDailyRevenue(result, loop)
}

function isiDailyRevenue(result, loop) {
    jQuery("#tabel-SalesBoard tbody").append(`<tr><td class="headcol">Daily Revenue</td></tr>`)
    for (let i = loop; i < result.sales + loop; i++) {
        let tables = `<td>${(result.hasil[i].revenueBudget / result.hasil[i].daysMonth).toFixed(2)}</td>
                      <td>${result.hasil[i].revenueDay}</td>
                      <td>${result.hasil[i].revenueBudget == 0 ? `0` : `${Math.round(((result.hasil[i].revenueDay / (result.hasil[i].revenueBudget / result.hasil[i].daysMonth) * 100) + Number.EPSILON) * 100) / 100}`}</td>`
        jQuery("#tabel-SalesBoard tbody tr:last-child").append(tables)
    }
}


function customScrolling() {
    var $th = $('.wraptable').find('thead th')
    $('.wraptable').on('scroll', function () {
        $th.css('transform', 'translateY(' + this.scrollTop + 'px)')
    });
}
