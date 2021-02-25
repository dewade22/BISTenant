
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
        jQuery('#tableBMIMonth tbody').empty();
        jQuery('#tableBMIMonthLiters tbody').empty();
        jQuery('#tableBMIYear tbody').empty();
        jQuery('#tableBMIYearlyLiters tbody').empty();
        jQuery('#summaryRevenue tbody').empty();
        jQuery('#summaryRevenueYear tbody').empty();
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
            $('#noDays').html(res.noDaysM);
            $('#totalDays').html(res.dayM);           
            $('#tglIni').html(`${myDate.getDate()}-${bulan[myDate.getMonth()]}`);
            $('#sdTglIni').html(`s/d : ${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
            $('#persenHari').html(`${Math.round(((res.noDaysM / res.dayM * 100) + Number.EPSILON) * 100) / 100}%`);
            $('#transactionDateYear').html(`${myDate.getDate()} ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#noDaysYear').html(res.noDaysY);
            $('#totalDaysYear').html(res.dayY);
            $('#BulanIni').html(`${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#sdBulanIni').html(`s/d: ${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#persenBulan').html(`${Math.round(((res.noDaysY / res.dayY * 100) + Number.EPSILON) * 100) / 100}%`);
            $('#BulanIniL').html(`${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#sdBulanIniL').html(`s/d : ${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
            $('#persenBulanL').html(`${Math.round(((res.noDaysY / res.dayY * 100) + Number.EPSILON) * 100) / 100} %`);
            $('#transactionDateSum').html(`Report Based on Transaction Date: ${myDate.getDate()} ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#transBulanSum').html(`Transaksi Bln ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#tglTransaksiSum').html(`${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
            $('#TargetBulan').html(`Target ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#sdtglTransaksiSum').html(`s/d: ${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
            $('#totalSisaSum').html(`Total Sisa di Bln ${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#rataSisaSum').html(`Rata2 Sisa perHari Bulan ${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#transactionDateSumYear').html(`Report Based on Transaction Date: ${myDate.getDate()} ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#transTahunSum').html(`Transaksi s/d Bln ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#tglTransaksiSumYear').html(`${bulan[myDate.getMonth()]}`);
            $('#sdtglTransaksiSumYear').html(`s/d : ${bulan[myDate.getMonth()]}`);
            $('#TargetTahun').html(`Target ${myDate.getFullYear()}`);
            $('#totalSisaSumYear').html(`Total Sisa di Tahun ${myDate.getFullYear()}`);
            $('#rataSisaSumYear').html(`Rata2 Sisa perBulan Tahun ${myDate.getFullYear()}`);
            insertSalesActual(date, res);
        },
    });
};
function insertSalesActual(date, res) {
    //Header untuk tgl di thead
    let parseDate = date.split('-');
    let myDate = new Date(parseDate[2], parseDate[0] - 1, parseDate[1]);

    $.ajax({
        type: 'GET',
        dataType: 'json',
        data: {
            DateTime: date
        },
        url: baseurl + '/BalimoonBMI/SalesActual/BMISalesAndTarget',
        success: function (data) {
            let totalToday = 0;
            let totalBulan = 0;
            let rataHari = 0;
            let targetBulan = 0;
            let rataHariTarget = 0;
            let result = data.salesActual.value;
            for (let i = 0; i < result.length; i++){
                let isi = `<tr>
                                <td>${result[i].category}</td>
                                <td>Rp. ${(result[i].todayRevenue).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2})}</td>
                                <td>Rp. ${(result[i].monthlyRevenue).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                <td>${Math.round(((result[i].monthlyRevenue / result[i].monthlyRevenueBudget*100)+Number.EPSILON)*100)/100} %</td>
                                <td>Rp. ${(result[i].monthlyRevenue / res.noDaysM).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                <td>Rp. ${(result[i].monthlyRevenueBudget).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                <td>Rp. ${(result[i].monthlyRevenueBudget / res.dayM).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                <td>Rp. ${(result[i].monthlyRevenueBudget - result[i].monthlyRevenue).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                <td>Rp. ${res.dayM - res.noDaysM == 0 ? (result[i].monthlyRevenueBudget - result[i].monthlyRevenue) : ((result[i].monthlyRevenueBudget - result[i].monthlyRevenue) / (res.dayM - res.noDaysM)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                           </tr>`
                jQuery("#tableBMIMonth tbody").append(isi);
                totalToday += result[i].todayRevenue;
                totalBulan += result[i].monthlyRevenue;
                rataHari += (result[i].monthlyRevenue / res.noDaysM);
                targetBulan += result[i].monthlyRevenueBudget;
                rataHariTarget += (result[i].monthlyRevenueBudget / res.dayM);

            }
            //Pemisah
            let pemisah = `<tr><td colspan="9"></td></tr>`
            jQuery("#tableBMIMonth tbody").append(pemisah);
            let LandedCost = data.landedCost.value;
            totalToday += LandedCost[0].todayLandedCost;
            totalBulan += LandedCost[0].monthLandedCost;
            rataHari += (LandedCost[0].monthLandedCost / res.noDaysM);
            //Isi Landed Cost
            let isitabel = `<tr>
                                <td>Landed Cost</td>
                                <td>Rp. ${(LandedCost[0].todayLandedCost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                <td>Rp. ${(LandedCost[0].monthLandedCost.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }))}</td>
                                <td></td>
                                <td>Rp. ${(LandedCost[0].monthLandedCost / res.noDaysM).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                                <td colspan="4"></td>
                            </tr>`
            jQuery("#tableBMIMonth tbody").append(isitabel);

            //Isi Total
            let rowTotal = `<tr>
                                <td style="font-weight:bold">Total</td>
                                <td style="font-weight:bold">Rp. ${totalToday.toLocaleString('en-US', {minimumFractionDigits: 2, maximumFractionDigits: 2})}</td>
                                <td style="font-weight:bold">Rp. ${totalBulan.toLocaleString('en-US', {minimumFractionDigits:2,maximumFractionDigits:2})}</td>
                                <td style="font-weight:bold">${Math.round(((totalBulan / targetBulan*100)+Number.EPSILON)*100)/100}%</td>
                                <td style="font-weight:bold">Rp. ${rataHari.toLocaleString('en-US', {minimumFractionDigits:2,maximumFractionDigits:2})}</td>
                                <td style="font-weight:bold">Rp. ${targetBulan.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                <td style="font-weight:bold">Rp. ${rataHariTarget.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                <td style="font-weight:bold">Rp. ${(targetBulan - totalBulan).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                                <td style="font-weight:bold">Rp. ${(res.dayM - res.noDaysM == 0 ? (targetBulan - totalBulan) : ((targetBulan - totalBulan) / (res.dayM - res.noDaysM))).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                            </tr>`
            jQuery("#tableBMIMonth tbody").append(rowTotal);
            //panggil Function untuk isi liter bulan dan dapatkan nilai total
            let MonthlySales = {
                ttlToday: totalToday,
                ttlBulan: totalBulan,
                rtHari: rataHari,
                tgBulan: targetBulan,
                rtHariTarget: rataHariTarget,
                rtSisa: (res.dayM - res.noDaysM == 0 ? (targetBulan - totalBulan) : ((targetBulan - totalBulan) / (res.dayM - res.noDaysM)))
            }
            let MonthlyLiters = insertSalesActualMonthLiters(myDate, res, result);
            let YearlySales = insertSalesActualYearly(myDate, res, data);
            let YearlyLiters = insertSalesActualYearlyLiters(myDate, res, data);
            insertSummaryRevenue(myDate, res, MonthlySales, MonthlyLiters);
            insertSummaryRevenueYearly(myDate, res, YearlySales, YearlyLiters);
        },
        
    })
};

function insertSalesActualMonthLiters(myDate, days, data) {
    
    $('#tglIniL').html(`${myDate.getDate()}-${bulan[myDate.getMonth()]}`);
    $('#sdTglIniL').html(`s/d : ${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
    $('#persenHariL').html(`${Math.round(((days.noDaysM / days.dayM * 100) + Number.EPSILON) * 100) / 100}%`);

    //Liter Item
    let totalTodayL = 0;
    let totalBulanL = 0;
    let rataHariL = 0;
    let targetBulanL = 0;
    let rataHariTargetL = 0;
    for (let i = 0; i < data.length; i++) {
        let product = `<tr>
                            <td>${data[i].category}</td>
                            <td>${data[i].todayLiters.toFixed(2)} L</td>
                            <td>${data[i].monthlyLiters.toFixed(2)} L</td>
                            <td>${Math.round(((data[i].monthlyLiters / data[i].monthlyLitersBudget*100)+Number.EPSILON)*100)/100} %</td>
                            <td>${(data[i].monthlyLiters/days.noDaysM).toFixed(2)} L</td>
                            <td>${data[i].monthlyLitersBudget.toFixed(2)} L</td>
                            <td>${(data[i].monthlyLitersBudget/days.dayM).toFixed(2)} L</td>
                            <td>${(data[i].monthlyLitersBudget - data[i].monthlyLiters).toFixed(2)} L</td>
                            <td>${(days.dayM - days.noDaysM == 0 ? (data[i].monthlyLitersBudget - data[i].monthlyLiters) : ((data[i].monthlyLitersBudget - data[i].monthlyLiters)/(days.dayM - days.noDaysM))).toFixed(2)} L</td>
                       </tr>`
        jQuery("#tableBMIMonthLiters tbody").append(product);
        totalTodayL += data[i].todayLiters;
        totalBulanL += data[i].monthlyLiters;
        rataHariL += (data[i].monthlyLiters / days.noDaysM);
        targetBulanL += data[i].monthlyLitersBudget;
        rataHariTargetL += (data[i].monthlyLitersBudget / days.dayM);
    }
    let pemisah = `<tr><td colspan="9"></td></tr>`
    jQuery("#tableBMIMonthLiters tbody").append(pemisah);

    //Total
    let totalLiters = `<tr>
                           <td style="font-weight:bold">Total</td>
                           <td style="font-weight:bold">${totalTodayL.toFixed(2)} L</td>
                           <td style="font-weight:bold">${totalBulanL.toFixed(2)} L</td>
                           <td style="font-weight:bold">${Math.round(((totalBulanL / targetBulanL*100)+Number.EPSILON)*100)/100} %</td>
                           <td style="font-weight:bold">${rataHariL.toFixed(2)} L</td>
                           <td style="font-weight:bold">${targetBulanL.toFixed(2)} L</td>
                           <td style="font-weight:bold">${rataHariTargetL.toFixed(2)} L</td>
                           <td style="font-weight:bold">${(targetBulanL-totalBulanL).toFixed(2)} L</td>
                           <td style="font-weight:bold">${((days.dayM - days.noDaysM) == 0 ? (targetBulanL-totalBulanL): (targetBulanL-totalBulanL)/(days.dayM-days.noDaysM)).toFixed(2) } L</td>
                       </tr>`
    jQuery("#tableBMIMonthLiters tbody").append(totalLiters);
    let totalMonthlyLiter = {
        ttlToday: totalTodayL,
        ttlBulan: totalBulanL,
        rtHari: rataHariL,
        tgBulan: targetBulanL,
        rtHariTarget: rataHariTargetL,
        rtSisa: ((days.dayM - days.noDaysM) == 0 ? (targetBulanL - totalBulanL) : (targetBulanL - totalBulanL) / (days.dayM - days.noDaysM))
    }
    return (totalMonthlyLiter)
    
}

function insertSalesActualYearly(date, days, datas) {
    //Untuk Yearly Total
    let totalBulan = 0;
    let totalTahun = 0;
    let rataBulan = 0;
    let targetTahun = 0;
    let rataBulanTarget = 0;

    //insert product
    let data = datas.salesActual.value;
    for (let i = 0; i < data.length; i++) {
        let products = `<tr>
                        <td>${data[i].category}</td>
                        <td>Rp. ${data[i].monthlyRevenue.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td>Rp. ${data[i].yearlyRevenue.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td>${Math.round(((data[i].yearlyRevenue / data[i].yearlyRevenueBudget*100)+Number.EPSILON)*100)/100} %</td>
                        <td>Rp. ${(data[i].yearlyRevenue / (date.getMonth() + 1)).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td>Rp. ${data[i].yearlyRevenueBudget.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td>Rp. ${(data[i].yearlyRevenueBudget / 12).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(data[i].yearlyRevenueBudget - data[i].yearlyRevenue).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td>Rp. ${(12 - (date.getMonth() + 1) == 0 ? (data[i].yearlyRevenueBudget - data[i].yearlyRevenue) : ((data[i].yearlyRevenueBudget - data[i].yearlyRevenue) / (12 - (date.getMonth() + 1)))).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    </tr>`
        jQuery("#tableBMIYear tbody").append(products);
        totalBulan += data[i].monthlyRevenue;
        totalTahun += data[i].yearlyRevenue;
        rataBulan += (data[i].yearlyRevenue / (date.getMonth() + 1));
        targetTahun += data[i].yearlyRevenueBudget;
        rataBulanTarget += (data[i].yearlyRevenueBudget / 12);
    }
    let pemisah = `<tr>
                      <td colspan="9"></td>
                   </tr>`
    jQuery("#tableBMIYear tbody").append(pemisah);

    //Tambah Landed Cost
    let LandedCost = datas.landedCost.value;
    totalBulan += LandedCost[0].monthLandedCost;
    totalTahun += LandedCost[0].yearLandedCost;
    rataBulan += (LandedCost[0].yearLandedCost / (date.getMonth() + 1));

    let addLandedCost = `<tr>
                            <td>Landed Cost</td>
                            <td>Rp. ${LandedCost[0].monthLandedCost.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                            <td>Rp. ${LandedCost[0].yearLandedCost.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                            <td></td>
                            <td>Rp. ${(LandedCost[0].yearLandedCost / (date.getMonth() + 1)).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                            <td colspan="4"></td>
                         </tr>`
    jQuery("#tableBMIYear tbody").append(addLandedCost);
    jQuery("#tableBMIYear tbody").append(pemisah);
    //Tambah Total
    let rowTotal = `<tr>
                        <td style="font-weight:bold">Total</td>
                        <td style="font-weight:bold">Rp. ${totalBulan.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td style="font-weight:bold">Rp. ${totalTahun.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits:2 })}</td>
                        <td style="font-weight:bold">${Math.round((((totalTahun / targetTahun)*100)+Number.EPSILON)*100)/100} %</td>
                        <td style="font-weight:bold">Rp. ${rataBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits:2 })}</td>
                        <td style="font-weight:bold">Rp. ${targetTahun.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td style="font-weight:bold">Rp. ${rataBulanTarget.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td style="font-weight:bold">Rp. ${(targetTahun - totalTahun).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                        <td style="font-weight:bold">Rp. ${(12 - (date.getMonth() + 1) == 0 ? (targetTahun - totalTahun) : ((targetTahun - totalTahun) / 12 - (date.getMonth() + 1))).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    </tr>`
    jQuery("#tableBMIYear tbody").append(rowTotal);
    let YearlyTotal = {
        ttlBulan: totalBulan,
        ttlTahun: totalTahun,
        rtbulan: rataBulan,
        tgTahun: targetTahun,
        rBTarget: rataBulanTarget,
        rtSisa: (12 - (date.getMonth() + 1)) == 0 ? (targetTahun - totalTahun) : ((targetTahun - totalTahun) /( 12 - (date.getMonth() + 1)))
    }
    return(YearlyTotal)
} 

function insertSalesActualYearlyLiters(date, days, datas) {
    //Liter Item
    let totalBulan = 0;
    let totalTahun = 0;
    let rataBulan = 0;
    let targetTahun = 0;
    let rataBulanTarget = 0;

    //insert product into table
    let data = datas.salesActual.value;
    for (let i = 0; i < data.length; i++) {
        let products = `<tr>
                            <td>${data[i].category}</td>
                            <td>${data[i].monthlyLiters.toFixed(2)} L</td>
                            <td>${data[i].yearlyLiters.toFixed(2)} L</td>
                            <td>${Math.round(((data[i].yearlyLiters / data[i].yearlyLitersBudget*100)+Number.EPSILON)*100)/100} %</td>
                            <td>${data[i].yearlyLiters/((date.getMonth()+1)).toFixed(2)} L</td>
                            <td>${data[i].yearlyLitersBudget.toFixed(2)} L</td>
                            <td>${(data[i].yearlyLitersBudget/12).toFixed(2)} L</td>
                            <td>${(data[i].yearlyLitersBudget - data[i].yearlyLiters).toFixed(2)}</td>
                            <td>${((12 - (date.getMonth() + 1)) == 0 ? (data[i].yearlyLitersBudget - data[i].yearlyLiters) : (data[i].yearlyLitersBudget - data[i].yearlyLiters) / (12 - (date.getMonth() + 1))).toFixed(2)} L</td>
                        </tr>`
        jQuery("#tableBMIYearlyLiters tbody").append(products);
        totalBulan += data[i].monthlyLiters;
        totalTahun += data[i].yearlyLiters;
        rataBulan += data[i].yearlyLiters / ((date.getMonth() + 1));
        targetTahun += data[i].yearlyLitersBudget;
        rataBulanTarget += (data[i].yearlyLitersBudget / 12);
    }
    let pemisah = `<tr><td colspan ="9"></td></tr>`
    jQuery("#tableBMIYearlyLiters tbody").append(pemisah);

    //Tambahkan Total
    let totalLiters = `<tr>
                           <td style="font-weight:bold">Total</td>
                           <td style="font-weight:bold">${totalBulan.toFixed(2)} L</td>
                           <td style="font-weight:bold">${totalTahun.toFixed(2)} L</td>
                           <td style="font-weight:bold">${Math.round(((totalTahun / targetTahun*100)+Number.EPSILON)*100)/100} %</td>
                           <td style="font-weight:bold">${rataBulan.toFixed(2)} L</td>
                           <td style="font-weight:bold">${targetTahun.toFixed(2)} L</td>
                           <td style="font-weight:bold">${rataBulanTarget.toFixed(2)} L</td>
                           <td style="font-weight:bold">${(targetTahun - totalTahun).toFixed(2)} L</td>
                           <td style="font-weight:bold">${((12 - (date.getMonth() + 1)) == 0 ? (targetTahun - totalTahun) : (targetTahun - totalTahun) / ((12 - (date.getMonth() + 1))).toFixed(2)).toFixed(2)} L</td>
                       </tr>`
    jQuery("#tableBMIYearlyLiters tbody").append(totalLiters);

    let YearlyTotalLiters = {
        ttlBulan: totalBulan,
        ttlTahun: totalTahun,
        rtbulan: rataBulan,
        tgTahun: targetTahun,
        rBTarget: rataBulanTarget,
        rtSisa: (12 - (date.getMonth() + 1) == 0 ? (targetTahun - totalTahun) : (targetTahun - totalTahun) / (12 - (date.getMonth() + 1)))
    }
    
    return (YearlyTotalLiters)
}

function insertSummaryRevenue(date, days, MonthlySales, MonthlyLiters) {
    let Data = `<tr>
                    <td rowspan="2">BMI : ${days.noDaysM} Of ${days.dayM} Days</td>
                    <td rowspan="2">${Math.round(((days.noDaysM / days.dayM*100)+Number.EPSILON)*100)/100} %</td>
                    <td>Rp. ${MonthlySales.ttlToday.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${MonthlySales.ttlBulan.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td style="background-color:${(MonthlySales.ttlBulan / MonthlySales.tgBulan * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((MonthlySales.ttlBulan / MonthlySales.tgBulan*100)+Number.EPSILON)*100)/100} %</td>
                    <td>Rp. ${MonthlySales.rtHari.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${MonthlySales.tgBulan.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${MonthlySales.rtHariTarget.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${(MonthlySales.tgBulan - MonthlySales.ttlBulan).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${MonthlySales.rtSisa.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                </tr>
                <tr>
                    <td>${MonthlyLiters.ttlToday.toFixed(2)} L</td>
                    <td>${MonthlyLiters.ttlBulan.toFixed(2)} L</td>
                    <td style="background-color:${(MonthlyLiters.ttlBulan / MonthlyLiters.tgBulan * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((MonthlyLiters.ttlBulan / MonthlyLiters.tgBulan * 100)+Number.EPSILON)*100)/100} %</td>
                    <td>${MonthlyLiters.rtHari.toFixed(2)} L</td>
                    <td>${MonthlyLiters.tgBulan.toFixed(2)} L</td>
                    <td>${MonthlyLiters.rtHariTarget.toFixed(2)} L</td>
                    <td>${(MonthlyLiters.tgBulan-MonthlyLiters.ttlBulan).toFixed(2)} L</td>
                    <td>${MonthlyLiters.rtSisa.toFixed(2)} L</td>
                </tr>`
    jQuery("#summaryRevenue tbody").append(Data);
}

function insertSummaryRevenueYearly(date, days, YearlySales, YearlyLiters) {
    let Data = `<tr>
                    <td rowspan="2">BMI : ${days.noDaysY} Of ${days.dayY}</td>
                    <td rowspan="2">${Math.round(((days.noDaysY/days.dayY*100)+Number.EPSILON)*100)/100} %</td>
                    <td>Rp. ${YearlySales.ttlBulan.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${YearlySales.ttlTahun.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td style="background-color:${(YearlySales.ttlTahun / YearlySales.tgTahun * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((YearlySales.ttlTahun / YearlySales.tgTahun * 100)+Number.EPSILON)*100)/100} %</td>
                    <td>Rp. ${YearlySales.rtbulan.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${YearlySales.tgTahun.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${YearlySales.rBTarget.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${(YearlySales.tgTahun - YearlySales.ttlTahun).toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                    <td>Rp. ${YearlySales.rtSisa.toLocaleString('en-US', {minimumFractionDigits:2, maximumFractionDigits:2})}</td>
                </tr>
                <tr>
                    <td>${YearlyLiters.ttlBulan.toFixed(2)} L</td>
                    <td>${YearlyLiters.ttlTahun.toFixed(2)} L</td>
                    <td style="background-color:${(YearlyLiters.ttlTahun / YearlyLiters.tgTahun * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((YearlyLiters.ttlTahun/YearlyLiters.tgTahun*100)+Number.EPSILON)*100)/100} %</td>
                    <td>${YearlyLiters.rtbulan.toFixed(2)} L</td>
                    <td>${YearlyLiters.tgTahun.toFixed(2)} L</td>
                    <td>${YearlyLiters.rBTarget.toFixed(2)} L</td>
                    <td>${(YearlyLiters.tgTahun - YearlyLiters.ttlTahun).toFixed(2)} L</td>
                    <td>${YearlyLiters.rtSisa.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits:2 })} L</td>
                </tr>`
    jQuery('#summaryRevenueYear tbody').append(Data);
}