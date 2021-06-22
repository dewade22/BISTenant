let dates = new Date();
let month = dates.getMonth() + 1;
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
        jQuery('#tableBIPMonth tbody').empty();
        jQuery('#tableBIPMonthLiters tbody').empty();
        jQuery('#tableBIPYear tbody').empty();
        jQuery('#tableBIPYearlyLiters tbody').empty();
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
        url: baseurl + '/BalimoonBIP/SalesActual/SalesAndTarget',
        success: function (result) {
            let salesActual = result.salesActual;
            $('#transactionDate').html(`${myDate.getDate()} ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#noDays').html(salesActual[0].daysNo);
            $('#totalDays').html(salesActual[0].daysMonth);
            $('#tglIni').html(`${myDate.getDate()}-${bulan[myDate.getMonth()]}`);
            $('#sdTglIni').html(`s/d : ${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
            $('#persenHari').html(`${Math.round(((salesActual[0].daysNo / salesActual[0].daysMonth * 100) + Number.EPSILON) * 100) / 100}%`);
            $('#transactionDateYear').html(`${myDate.getDate()} ${bulan[myDate.getMonth()]} ${myDate.getFullYear()}`);
            $('#noDaysYear').html(salesActual[0].daysThisMonth);
            $('#totalDaysYear').html(salesActual[0].daysYear);
            $('#BulanIni').html(`${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#sdBulanIni').html(`s/d: ${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#persenBulan').html(`${Math.round(((salesActual[0].daysThisMonth / salesActual[0].daysYear * 100) + Number.EPSILON) * 100) / 100}%`);
            $('#BulanIniL').html(`${bulan[myDate.getMonth()]} - ${myDate.getFullYear()}`);
            $('#sdBulanIniL').html(`s/d : ${myDate.getDate()} - ${bulan[myDate.getMonth()]}`);
            $('#persenBulanL').html(`${Math.round(((salesActual[0].daysThisMonth / salesActual.daysYear * 100) + Number.EPSILON) * 100) / 100} %`);
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

            let monthlyRevenue = MonthlyRevenueBIP(myDate, result);
            let monthlyLiters = MonthlyLitersBIP(myDate, result);
            let yearlyRevenue = YearlyRevenueBIP(myDate, result);
            let yearlyLiters = YearlyLitersBIP(myDate, result);


            SummaryMonth(myDate, monthlyRevenue, monthlyLiters, result);
            SummaryYear(myDate, yearlyRevenue, yearlyLiters, result);
        },
        error: function (jqXHR, exception) {
            Swal.fire(
                'Error !',
                '' + exception + ' ' + jqXHR.status,
                'error'
            )
        }
    });
};

function MonthlyRevenueBIP(myDate, result) {
    let sales = result.salesActual;
    let dates = myDate.getMonth() + 1 + '-' + myDate.getDate() + '-' + myDate.getFullYear();
    //Untuk dijadikan total
    let totalToday = 0;
    let totalBulan = 0;
    let rataHari = 0;
    let targetBulan = 0;
    let rataHariTarget = 0;
    //Perulangan untuk tabel
    for (let i = 0; i < sales.length; i++) {
        let product = `<tr>
                        <td>${sales[i].itemCategory}</td>
                        <td>${sales[i].revenueDay == 0 ? `RP. ${(sales[i].revenueDay).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}` : `<a href="${localStorage.getItem('thisAddress')}/SalesActual/TodaySalesBIP?dateTime=${dates}&category=${sales[i].itemCategory}">Rp. ${(sales[i].revenueDay).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</a>`}</td>
                        <td>${sales[i].revenueMonth == 0 ? `Rp. ${(sales[i].revenueMonth).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}` : `<a href="${localStorage.getItem('thisAddress')}/SalesActual/MonthlySalesBIP?dateTime=${dates}&category=${sales[i].itemCategory}">Rp. ${(sales[i].revenueMonth).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</a>`}</td>
                        <td>${Math.round(((sales[i].revenueMonth / sales[i].revenueBudget * 100) + Number.EPSILON) * 100) / 100} %</td>
                        <td>Rp. ${(sales[i].revenueMonth / sales[i].daysNo).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(sales[i].revenueBudget).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(sales[i].revenueBudget / sales[i].daysMonth).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(sales[i].revenueBudget - sales[i].revenueMonth).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${sales[i].daysMonth - sales[i].daysNo == 0 ? (sales[i].revenueBudget - sales[i].revenueMonth) : ((sales[i].revenueBudget - sales[i].revenueMonth) / (sales[i].daysMonth - sales[i].daysNo)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                   </tr>`;
        jQuery("#tableBIPMonth tbody").append(product);
        totalToday += sales[i].revenueDay;
        totalBulan += sales[i].revenueMonth;
        rataHari += (sales[i].revenueMonth / sales[i].daysNo);
        targetBulan += sales[i].revenueBudget;
        rataHariTarget += (sales[i].revenueBudget / sales[i].daysMonth);
    }
    //Pemisah
    let pemisah = `<tr><td colspan="9"></td></tr>`
    jQuery("#tableBIPMonth tbody").append(pemisah);

    //Landed Cost
    let landC = result.landedCost;
    totalToday += landC[0].landedCost;
    totalBulan += landC[0].landedCostMonth;
    rataHari += (landC[0].landedCostMonth / sales[0].daysNo);
    let totalLC = `<tr>
                        <td>Landed Cost</td>
                        <td>Rp. ${(landC[0].landedCost).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(landC[0].landedCostMonth.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }))}</td>
                        <td></td>
                        <td>Rp. ${(landC[0].landedCostMonth / sales[0].daysNo).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td colspan="4"></td>
                   </tr>`;
    jQuery("#tableBIPMonth tbody").append(totalLC);

    jQuery("#tableBIPMonth tbody").append(pemisah);
    //Total
    let rowTotal = `<tr>
                        <td style="font-weight:bold">Total</td>
                        <td style="font-weight:bold">${totalToday == 0 ? `RP. ${(totalToday).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}` : `<a href="${localStorage.getItem('thisAddress')}/SalesActual/TodaySalesBIP?dateTime=${dates}&category=">Rp. ${(totalToday).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</a>`}</td>
                        <td style="font-weight:bold">${totalBulan == 0 ? `Rp. ${(totalBulan).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}` : `<a href="${localStorage.getItem('thisAddress')}/SalesActual/MonthlySalesBIP?dateTime=${dates}&category=">Rp. ${(totalBulan).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</a>`}</td>
                        <td style="font-weight:bold">${Math.round(((totalBulan / targetBulan * 100) + Number.EPSILON) * 100) / 100}%</td>
                        <td style="font-weight:bold">Rp. ${rataHari.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${targetBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${rataHariTarget.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${(targetBulan - totalBulan).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${(sales[0].daysMonth - sales[0].daysNo == 0 ? (targetBulan - totalBulan) : ((targetBulan - totalBulan) / (sales[0].daysMonth - sales[0].daysNo))).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    </tr>`
    jQuery("#tableBIPMonth tbody").append(rowTotal);

    //rekap
    let MonthlyRevenue = {
        totalToday: totalToday,
        totalBulan: totalBulan,
        rataHari: rataHari,
        targetBulan: targetBulan,
        rataHariTarget: rataHariTarget,
        rataSisa: (sales[0].daysMonth - sales[0].daysNo == 0 ? (targetBulan - totalBulan) : ((targetBulan - totalBulan) / (sales[0].daysMonth - sales[0].daysNo)))
    }

    //return rekap
    return MonthlyRevenue;
}

function MonthlyLitersBIP(myDate, result) {
    let Liters = result.salesActual;
    //untuk dijadikan total
    let totalToday = 0;
    let totalBulan = 0;
    let rataHari = 0;
    let targetBulan = 0;
    let rataHariTarget = 0;
    for (let i = 0; i < Liters.length; i++) {
        let product = `<tr>
                            <td>${Liters[i].itemCategory}</td>
                            <td>${Liters[i].litersDay.toFixed(2)} L</td>
                            <td>${Liters[i].litersMonth.toFixed(2)} L</td>
                            <td>${Math.round(((Liters[i].litersMonth / Liters[i].litersBudget * 100) + Number.EPSILON) * 100) / 100} %</td>
                            <td>${(Liters[i].litersMonth / Liters[i].daysNo).toFixed(2)} L</td>
                            <td>${Liters[i].litersBudget.toFixed(2)} L</td>
                            <td>${(Liters[i].litersBudget / Liters[i].daysMonth).toFixed(2)} L</td>
                            <td>${(Liters[i].litersBudget - Liters[i].litersMonth).toFixed(2)} L</td>
                            <td>${(Liters[i].daysMonth - Liters[i].daysNo == 0 ? (Liters[i].litersBudget - Liters[i].litersMonth) : ((Liters[i].litersBudget - Liters[i].litersMonth) / (Liters[i].daysMonth - Liters[i].daysNo))).toFixed(2)} L</td>
                       </tr>`
        jQuery("#tableBIPMonthLiters tbody").append(product);
        totalToday += Liters[i].litersDay;
        totalBulan += Liters[i].litersMonth;
        rataHari += (Liters[i].litersMonth / Liters[i].daysNo);
        targetBulan += Liters[i].litersBudget;
        rataHariTarget += (Liters[i].litersBudget / Liters[i].daysMonth);
    }

    //Pemisah
    let pemisah = `<tr><td colspan="9"></td></tr>`
    jQuery("#tableBIPMonthLiters tbody").append(pemisah);


    //Total
    let totalLiters = `<tr>
                           <td style="font-weight:bold">Total</td>
                           <td style="font-weight:bold">${totalToday.toFixed(2)} L</td>
                           <td style="font-weight:bold">${totalBulan.toFixed(2)} L</td>
                           <td style="font-weight:bold">${Math.round(((totalBulan / targetBulan * 100) + Number.EPSILON) * 100) / 100} %</td>
                           <td style="font-weight:bold">${rataHari.toFixed(2)} L</td>
                           <td style="font-weight:bold">${targetBulan.toFixed(2)} L</td>
                           <td style="font-weight:bold">${rataHariTarget.toFixed(2)} L</td>
                           <td style="font-weight:bold">${(targetBulan - totalBulan).toFixed(2)} L</td>
                           <td style="font-weight:bold">${((Liters[0].daysMonth - Liters[0].daysNo) == 0 ? (targetBulan - totalBulan) : (targetBulan - totalBulan) / (Liters[0].daysMonth - Liters[0].daysNo)).toFixed(2)} L</td>
                       </tr>`
    jQuery("#tableBIPMonthLiters tbody").append(totalLiters);

    //Rekap
    let MonthlyLiters = {
        totalToday: totalToday,
        totalBulan: totalBulan,
        rataHari: rataHari,
        targetBulan: targetBulan,
        rataHariTarget: rataHariTarget,
        rataSisa: (Liters[0].daysMonth - Liters[0].daysNo == 0 ? (targetBulan - totalBulan) : ((targetBulan - totalBulan) / (Liters[0].daysMonth - Liters[0].daysNo)))
    }

    //Return Rekap
    return MonthlyLiters;
}

function YearlyRevenueBIP(myDate, result) {
    let sales = result.salesActual;
    //Untuk dijadikan total
    let totalBulan = 0;
    let totalTahun = 0;
    let rataBulan = 0;
    let targetTahun = 0;
    let rataBulanTarget = 0;
    //Perulangan untuk tabel
    for (let i = 0; i < sales.length; i++) {
        let product = `<tr>
                        <td>${sales[i].itemCategory}</td>
                        <td>Rp. ${(sales[i].revenueMonth).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(sales[i].revenueYear).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>${Math.round(((sales[i].revenueYear / sales[i].revenueYearlyBudget * 100) + Number.EPSILON) * 100) / 100} %</td>
                        <td>Rp. ${(sales[i].revenueYear / (myDate.getMonth() + 1)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(sales[i].revenueYearlyBudget).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(sales[i].revenueYearlyBudget / 12).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(sales[i].revenueYearlyBudget - sales[i].revenueYear).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${12 - (myDate.getMonth() + 1) == 0 ? (sales[i].revenueYearlyBudget - sales[i].revenueYear) : ((sales[i].revenueYearlyBudget - sales[i].revenueYear) / (12 - (myDate.getMonth() + 1))).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                   </tr>`;
        jQuery("#tableBIPYear tbody").append(product);
        totalBulan += sales[i].revenueMonth;
        totalTahun += sales[i].revenueYear;
        rataBulan += (sales[i].revenueYear / (myDate.getMonth() + 1));
        targetTahun += sales[i].revenueYearlyBudget;
        rataBulanTarget += (sales[i].revenueYearlyBudget / 12);
    }

    //Pemisah
    let pemisah = `<tr><td colspan="9"></td></tr>`
    jQuery("#tableBIPYear tbody").append(pemisah);

    //Landed Cost
    let landC = result.landedCost;
    totalBulan += landC[0].landedCostMonth;
    totalTahun += landC[0].landedCostYear;
    rataBulan += (landC[0].landedCostYear / (myDate.getMonth() + 1));
    let totalLC = `<tr>
                        <td>Landed Cost</td>
                        <td>Rp. ${(landC[0].landedCostMonth).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td>Rp. ${(landC[0].landedCostYear.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }))}</td>
                        <td></td>
                        <td>Rp. ${(landC[0].landedCostYear / (myDate.getMonth() + 1)).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td colspan="4"></td>
                   </tr>`;
    jQuery("#tableBIPYear tbody").append(totalLC);

    jQuery("#tableBIPYear tbody").append(pemisah);
    //Total
    let rowTotal = `<tr>
                        <td style="font-weight:bold">Total</td>
                        <td style="font-weight:bold">Rp. ${totalBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${totalTahun.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">${Math.round(((totalTahun / targetTahun * 100) + Number.EPSILON) * 100) / 100}%</td>
                        <td style="font-weight:bold">Rp. ${rataBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${targetTahun.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${rataBulanTarget.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${(targetTahun - totalTahun).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                        <td style="font-weight:bold">Rp. ${(12 - (myDate.getMonth() + 1) == 0 ? (targetTahun - totalTahun) : ((targetTahun - totalTahun) / (12 - (myDate.getMonth() + 1)))).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    </tr>`
    jQuery("#tableBIPYear tbody").append(rowTotal);

    //rekap
    let YearlyRevenue = {
        totalBulan: totalBulan,
        totalTahun: totalTahun,
        rataBulan: rataBulan,
        targetTahun: targetTahun,
        rataBulanTarget: rataBulanTarget,
        rataSisa: (12 - (myDate.getMonth() + 1) == 0 ? (targetTahun - totalTahun) : ((targetTahun - totalTahun) / (12 - (myDate.getMonth() + 1))))
    }

    //return rekap
    return YearlyRevenue;
}

function YearlyLitersBIP(myDate, result) {
    let Liters = result.salesActual;
    //Untuk dijadikan total
    let totalBulan = 0;
    let totalTahun = 0;
    let rataBulan = 0;
    let targetTahun = 0;
    let rataBulanTarget = 0;
    for (let i = 0; i < Liters.length; i++) {
        let product = `<tr>
                            <td>${Liters[i].itemCategory}</td>
                            <td>${Liters[i].litersMonth.toFixed(2)} L</td>
                            <td>${Liters[i].litersYear.toFixed(2)} L</td>
                            <td>${Math.round(((Liters[i].litersYear / Liters[i].litersyearlyBudget * 100) + Number.EPSILON) * 100) / 100} %</td>
                            <td>${(Liters[i].litersYear / (myDate.getMonth() + 1))} L</td>
                            <td>${Liters[i].litersyearlyBudget.toFixed(2)} L</td>
                            <td>${(Liters[i].litersyearlyBudget / 12).toFixed(2)} L</td>
                            <td>${(Liters[i].litersyearlyBudget - Liters[i].litersYear).toFixed(2)} L</td>
                            <td>${(12 - (myDate.getMonth() + 1) == 0 ? (Liters[i].litersyearlyBudget - Liters[i].litersYear) : ((Liters[i].litersyearlyBudget - Liters[i].litersYear) / (12 - (myDate.getMonth() + 1)))).toFixed(2)} L</td>
                       </tr>`
        jQuery("#tableBIPYearlyLiters tbody").append(product);
        totalBulan += Liters[i].litersMonth;
        totalTahun += Liters[i].litersYear;
        rataBulan += (Liters[i].litersYear / (myDate.getMonth() + 1));
        targetTahun += Liters[i].litersyearlyBudget;
        rataBulanTarget += (Liters[i].litersyearlyBudget / 12);
    }

    //Pemisah
    let pemisah = `<tr><td colspan="9"></td></tr>`
    jQuery("#tableBIPYearlyLiters tbody").append(pemisah);

    //Total
    let totalLiters = `<tr>
                           <td style="font-weight:bold">Total</td>
                           <td style="font-weight:bold">${totalBulan.toFixed(2)} L</td>
                           <td style="font-weight:bold">${totalTahun.toFixed(2)} L</td>
                           <td style="font-weight:bold">${Math.round(((totalTahun / targetTahun * 100) + Number.EPSILON) * 100) / 100} %</td>
                           <td style="font-weight:bold">${rataBulan.toFixed(2)} L</td>
                           <td style="font-weight:bold">${targetTahun.toFixed(2)} L</td>
                           <td style="font-weight:bold">${rataBulanTarget.toFixed(2)} L</td>
                           <td style="font-weight:bold">${(targetTahun - totalTahun).toFixed(2)} L</td>
                           <td style="font-weight:bold">${((12 - (myDate.getMonth() + 1)) == 0 ? (targetTahun - totalTahun) : (targetTahun - totalTahun) / (12 - (myDate.getMonth() + 1))).toFixed(2)} L</td>
                       </tr>`
    jQuery("#tableBIPYearlyLiters tbody").append(totalLiters);

    //rekap
    let YearlyLiters = {
        totalBulan: totalBulan,
        totalTahun: totalTahun,
        rataBulan: rataBulan,
        targetTahun: targetTahun,
        rataBulanTarget: rataBulanTarget,
        rataSisa: (12 - (myDate.getMonth() + 1) == 0 ? (targetTahun - totalTahun) : ((targetTahun - totalTahun) / (12 - (myDate.getMonth() + 1))))
    }

    //return rekap
    return YearlyLiters;
}

function SummaryMonth(myDate, monthlyRevenue, MonthlyLiters, result) {
    let Data = `<tr>
                    <td rowspan="2">BMI : ${result.salesActual[0].daysNo} Of ${result.salesActual[0].daysMonth} Days</td>
                    <td rowspan="2">${Math.round(((result.salesActual[0].daysNo / result.salesActual[0].daysMonth * 100) + Number.EPSILON) * 100) / 100} %</td>
                    <td>Rp. ${monthlyRevenue.totalToday.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${monthlyRevenue.totalBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="background-color:${(monthlyRevenue.totalBulan / monthlyRevenue.targetBulan * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((monthlyRevenue.totalBulan / monthlyRevenue.targetBulan * 100) + Number.EPSILON) * 100) / 100} %</td>
                    <td>Rp. ${monthlyRevenue.rataHari.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${monthlyRevenue.targetBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${monthlyRevenue.rataHariTarget.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${(monthlyRevenue.targetBulan - monthlyRevenue.totalBulan).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${monthlyRevenue.rataSisa.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                </tr>
                <tr>
                    <td>${MonthlyLiters.totalToday.toFixed(2)} L</td>
                    <td>${MonthlyLiters.totalBulan.toFixed(2)} L</td>
                    <td style="background-color:${(MonthlyLiters.totalBulan / MonthlyLiters.targetBulan * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((MonthlyLiters.totalBulan / MonthlyLiters.targetBulan * 100) + Number.EPSILON) * 100) / 100} %</td>
                    <td>${MonthlyLiters.rataHari.toFixed(2)} L</td>
                    <td>${MonthlyLiters.targetBulan.toFixed(2)} L</td>
                    <td>${MonthlyLiters.rataHariTarget.toFixed(2)} L</td>
                    <td>${(MonthlyLiters.targetBulan - MonthlyLiters.totalBulan).toFixed(2)} L</td>
                    <td>${MonthlyLiters.rataSisa.toFixed(2)} L</td>
                </tr>`
    jQuery("#summaryRevenue tbody").append(Data);
}

function SummaryYear(myDate, YearlyRevenue, YearlyLiters, result) {
    let Data = `<tr>
                    <td rowspan="2">BMI : ${result.salesActual[0].daysThisMonth} Of ${result.salesActual[0].daysYear}</td>
                    <td rowspan="2">${Math.round(((result.salesActual[0].daysThisMonth / result.salesActual[0].daysYear * 100) + Number.EPSILON) * 100) / 100} %</td>
                    <td>Rp. ${YearlyRevenue.totalBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${YearlyRevenue.totalTahun.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td style="background-color:${(YearlyRevenue.totalTahun / YearlyRevenue.targetTahun * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((YearlyRevenue.totalTahun / YearlyRevenue.targetTahun * 100) + Number.EPSILON) * 100) / 100} %</td>
                    <td>Rp. ${YearlyRevenue.rataBulan.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${YearlyRevenue.targetTahun.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${YearlyRevenue.rataBulanTarget.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${(YearlyRevenue.targetTahun - YearlyRevenue.totalTahun).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                    <td>Rp. ${YearlyRevenue.rataSisa.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</td>
                </tr>
                <tr>
                    <td>${YearlyLiters.totalBulan.toFixed(2)} L</td>
                    <td>${YearlyLiters.totalTahun.toFixed(2)} L</td>
                    <td style="background-color:${(YearlyLiters.totalTahun / YearlyLiters.targetTahun * 100) < 50 ? '#ff4f42' : '#03fc17'}">${Math.round(((YearlyLiters.totalTahun / YearlyLiters.targetTahun * 100) + Number.EPSILON) * 100) / 100} %</td>
                    <td>${YearlyLiters.rataBulan.toFixed(2)} L</td>
                    <td>${YearlyLiters.targetTahun.toFixed(2)} L</td>
                    <td>${YearlyLiters.rataBulanTarget.toFixed(2)} L</td>
                    <td>${(YearlyLiters.targetTahun - YearlyLiters.totalTahun).toFixed(2)} L</td>
                    <td>${YearlyLiters.rataSisa.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })} L</td>
                </tr>`
    jQuery('#summaryRevenueYear tbody').append(Data);
}