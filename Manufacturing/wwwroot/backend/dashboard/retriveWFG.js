if (tenant.toLowerCase() != "balimoon" || tenant.toLowerCase().includes('bml')) {
    $('#panelRaw').hide()
}


var dates = new Date();
const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"];
var month = monthNames[dates.getMonth()];
var year = dates.getFullYear();
$("#stokPer").html('Stock on '+month + " - " + year);
//let tenant = $('#tenantActive').val().trim();
//let baseurl = window.location.origin;
//let alamat = baseurl + '/' + tenant + '/Dashboard';
//Get Spirit
(function Spirit() {
    $.ajax({
        type: 'GET',
        url: alamat + '/getSpirit?tenant=' + tenant,
        success: function (res) {
            if (tenant.toLowerCase() === 'balimoon' || tenant.toLowerCase() === 'training' || tenant.toLowerCase().includes('bml')) {
                $.each(res, function (i) {
                    //whisky
                    if (res[i].productGroup.toLowerCase() == "whisky" && res[i].kemasan.toLowerCase() == 0.7) {
                        $('#omrach700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    else if (res[i].productGroup.toLowerCase() == "whisky" && res[i].kemasan.toLowerCase() == 0.25) {
                        $('#omrach250').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Vodka 9
                    else if (res[i].productGroup.toLowerCase() == "vodka" && res[i].kemasan.toLowerCase() == 0.7) {
                        $('#9vd700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Silver
                    else if (res[i].productGroup.toLowerCase() == "gin" && res[i].kemasan.toLowerCase() == 0.7) {
                        $('#silver700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Blanco
                    //250
                    else if (res[i].productGroup.toLowerCase() == "blanco" && res[i].kemasan.toLowerCase() == 0.25) {
                        $('#blanco250').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //700
                    else if (res[i].productGroup.toLowerCase() == "blanco" && res[i].kemasan.toLowerCase() == 0.7) {
                        $('#blanco700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //5000
                    else if (res[i].productGroup.toLowerCase() == "blanco" && res[i].kemasan.toLowerCase() == 5) {
                        $('#blanco5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Blue Vodka
                    //250
                    else if (res[i].productGroup.toLowerCase() == "bluevodka" && res[i].kemasan.toLowerCase() == 0.25) {
                        $('#bmvd25').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //700
                    else if (res[i].productGroup.toLowerCase() == "bluevodka" && res[i].kemasan.toLowerCase() == 0.7) {
                        $('#bmvd7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //5000
                    else if (res[i].productGroup.toLowerCase() == "bluevodka" && res[i].kemasan.toLowerCase() == 5) {
                        $('#bmvd5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //total Liqueurs
                    //5000
                    else if (res[i].productGroup.toLowerCase() == "liqueurs" && res[i].kemasan.toLowerCase() == 5) {
                        $('#Liqtot5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //750
                    else if (res[i].productGroup.toLowerCase() == "liqueurs" && res[i].kemasan.toLowerCase() == 0.75) {
                        $('#Liqtot75').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //700
                    else if (res[i].productGroup.toLowerCase() == "liqueurs" && res[i].kemasan.toLowerCase() == 0.7) {
                        $('#Liqtot7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }

                })
            } else if (tenant.toLowerCase().includes('bmi')) {
                $.each(res, function (i) {
                   //700
                   //Omrach
                   if (res[i].productGroup.toLowerCase() == 'whisky' && res[i].kemasan.toLowerCase().indexOf('700') != -1) {
                       $('#omrach700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //250
                   else if (res[i].productGroup.toLowerCase() == 'whisky' && res[i].kemasan.toLowerCase().indexOf('250') != -1) {
                       $('#omrach250').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Vodka 9 700
                   else if (res[i].productGroup.toLowerCase() == "vodka" && res[i].kemasan.toLowerCase().indexOf('700') != -1 && res[i].productGroupCode == 9) {
                       $('#9vd700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Silver Gin 700
                   else if (res[i].productGroup.toLowerCase() == "gin" && res[i].kemasan.toLowerCase().indexOf('700') != -1) {
                       $('#silver700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Blanco 250
                   else if (res[i].productGroup.toLowerCase() == "blanco" && res[i].kemasan.toLowerCase().indexOf('250') != -1) {
                       $('#blanco250').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Blanco 700
                   else if (res[i].productGroup.toLowerCase() == "blanco" && res[i].kemasan.toLowerCase().indexOf('700') != -1) {
                       $('#blanco700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Blanco 5000
                   else if (res[i].productGroup.toLowerCase() == "blanco" && res[i].kemasan.toLowerCase().indexOf('5000') != -1) {
                       $('#blanco5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Blue Vodka 250
                   else if (res[i].productGroup.toLowerCase() == "vodka" && res[i].kemasan.toLowerCase().indexOf('250') != -1 && res[i].productGroupCode.toLowerCase() == 'bm') {
                       $('#bmvd25').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Blue Vodka 700
                   else if (res[i].productGroup.toLowerCase() == "vodka" && res[i].kemasan.toLowerCase().indexOf('700') != -1 && res[i].productGroupCode.toLowerCase() == 'bm') {
                       $('#bmvd7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Blue Vodka 5000
                   else if (res[i].productGroup.toLowerCase() == "vodka" && res[i].kemasan.toLowerCase().indexOf('5000') != -1 && res[i].productGroupCode.toLowerCase() == 'bm') {
                       $('#bmvd5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Total Liq 350
                   else if (res[i].productGroup.toLowerCase() == "liqueurs" && res[i].kemasan.toLowerCase().indexOf('350') != -1) {
                       $('#Liqtot35').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Total Liq 700
                   else if (res[i].productGroup.toLowerCase() == "liqueurs" && res[i].kemasan.toLowerCase().indexOf('700') != -1) {
                       $('#Liqtot7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Total Liq 750
                   else if (res[i].productGroup.toLowerCase() == "liqueurs" && res[i].kemasan.toLowerCase().indexOf('750') != -1) {
                       $('#Liqtot75').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                   //Total Liq 5000
                   else if (res[i].productGroup.toLowerCase() == "liqueurs" && res[i].kemasan.toLowerCase().indexOf('5000') != -1) {
                       $('#Liqtot5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                   }
                })
            } else{
                       $.each(res, function (i) {
                           //Omrach
                           if (res[i].productGroup.toLowerCase() == 'whisky') {
                               if (res[i].kemasan == 0.7) {
                                   $('#omrach700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 0.25) {
                                   $('#omrach250').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                           }
                           else if (res[i].productGroup.toLowerCase() == 'vodka') {
                               if (res[i].kemasan == 0.7) {
                                   $('#9vd700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                           }
                           else if (res[i].productGroup.toLowerCase() == 'gin') {
                               if (res[i].kemasan == 0.7) {
                                   $('#silver700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                           }
                           else if (res[i].productGroup.toLowerCase() == 'blanco') {
                               if (res[i].kemasan == 0.25) {
                                   $('#blanco250').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 0.7) {
                                   $('#blanco700').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 5) {
                                   $('#blanco5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                           }
                           else if (res[i].productGroup.toLowerCase() == 'vodka bm') {
                               if (res[i].kemasan == 0.25) {
                                   $('#bmvd25').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 0.7) {
                                   $('#bmvd7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 5) {
                                   $('#bmvd5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                           }
                           else if (res[i].productGroup.toLowerCase() == 'teq') {
                               if (res[i].kemasan == 0.25) {
                                   $('#teq25').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 0.7) {
                                   $('#teq7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 0.5) {
                                   $('#teq5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                           }
                           else if (res[i].productGroup.toLowerCase() == 'liqueurs') {
                               if (res[i].kemasan == 0.7) {
                                   $('#Liqtot7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 0.75) {
                                   $('#Liqtot75').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                               else if (res[i].kemasan == 5) {
                                   $('#Liqtot5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                               }
                           }

                       })
                   } 
          
        }
    })
    
})();

$('#panelLiq').click(function () {
    $.ajax({
        type: 'GET',
        url: alamat + '/getLiq?tenant=' + tenant,
        success: function (res) {
            if (tenant.toLowerCase().includes('bip')) {
                $.each(res, function (i) {
                    if (res[i].productGroup.toLowerCase().includes('ts')) {
                        if (res[i].kemasan == 0.7) {
                            $('#ts7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#ts5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('bc')) {
                        if (res[i].kemasan == 0.7) {
                            $('#bc7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#bc5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('cf')) {
                        if (res[i].kemasan == 0.7) {
                            $('#cof7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#cof5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('cn')) {
                        if (res[i].kemasan == 0.7) {
                            $('#coco7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#coco5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('bk')) {
                        if (res[i].kemasan == 0.7) {
                            $('#bk7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#bk5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('bn')) {
                        if (res[i].kemasan == 0.7) {
                            $('#bn7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#bn5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('bs')) {
                        if (res[i].kemasan == 0.7) {
                            $('#bs7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#bs5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('hz')) {
                        if (res[i].kemasan == 0.7) {
                            $('#hz7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                        else if (res[i].kemasan == 5) {
                            $('#hz5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                        }
                    }
                    else if (res[i].productGroup.toLowerCase().includes('lc')) {
                        if (res[i].kemasan == 0.7) {
                            $("#lc7").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                        }
                        else if (res[i].kemasan == 5) {
                            $("#lc5").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                        }
                    }
                })
            }
            else {
                $.each(res, function (i) {
                    //Triple Sec 700ml
                    if (res[i].liqGroup.toLowerCase() == 'ts' && res[i].kemasan == 0.7) {
                        $('#ts7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //triple sec 5000
                    else if (res[i].liqGroup.toLowerCase() == 'ts' && res[i].kemasan == 5) {
                        $('#ts5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Blue Curacao 700
                    else if (res[i].liqGroup.toLowerCase() == 'bc' && res[i].kemasan == 0.7) {
                        $('#bc7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Blue Curacao 5000
                    else if (res[i].liqGroup.toLowerCase() == 'bc' && res[i].kemasan == 5) {
                        $('#bc5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Coffee 700
                    else if (res[i].liqGroup.toLowerCase() == 'cf' && res[i].kemasan == 0.7) {
                        $('#cof7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //coffee 5000
                    else if (res[i].liqGroup.toLowerCase() == 'cf' && res[i].kemasan == 5) {
                        $('#cof5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Coconut 700
                    else if (res[i].liqGroup.toLowerCase() == 'cn' && res[i].kemasan == 0.7) {
                        $('#coco7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Coconut 5000
                    else if (res[i].liqGroup.toLowerCase() == 'cn' && res[i].kemasan == 5) {
                        $('#coco5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Black Currant 700
                    else if (res[i].liqGroup.toLowerCase() == 'bk' && res[i].kemasan == 0.7) {
                        $('#bk7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Black Currant 5000
                    else if (res[i].liqGroup.toLowerCase() == 'bk' && res[i].kemasan == 5) {
                        $('#bk5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Banana 700
                    else if (res[i].liqGroup.toLowerCase() == 'bn' && res[i].kemasan == 0.7) {
                        $('#bn7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Banana 5000
                    else if (res[i].liqGroup.toLowerCase() == 'bn' && res[i].kemasan == 5) {
                        $('#bn5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Butter Scotch 700
                    else if (res[i].liqGroup.toLowerCase() == 'bs' && res[i].kemasan == 0.7) {
                        $('#bs7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Butter Scotch 5000
                    else if (res[i].liqGroup.toLowerCase() == 'bs' && res[i].kemasan == 5) {
                        $('#bs5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Hazzelnut 700
                    else if (res[i].liqGroup.toLowerCase() == 'hz' && res[i].kemasan == 0.7) {
                        $('#hz7').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Hazzelnut 5000
                    else if (res[i].liqGroup.toLowerCase() == 'hz' && res[i].kemasan == 5) {
                        $('#hz5').html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"))
                    }
                    //Lychee 700
                    else if (res[i].liqGroup.toLowerCase() == 'lc' && res[i].kemasan == 0.7) {
                        $("#lc7").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    //Lychee 5000
                    else if (res[i].liqGroup.toLowerCase() == 'lc' && res[i].kemasan == 5) {
                        $("#lc5").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    //Melon 700
                    else if (res[i].liqGroup.toLowerCase() == 'ml' && res[i].kemasan == 0.7) {
                        $("#ml7").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    //Melon 5000
                    else if (res[i].liqGroup.toLowerCase() == 'ml' && res[i].kemasan == 5) {
                        $("#ml5").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    // Pineaplle 700
                    else if (res[i].liqGroup.toLowerCase() == 'pn' && res[i].kemasan == 0.7) {
                        $("#pn7").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    // Pineaplle 5000
                    else if (res[i].liqGroup.toLowerCase() == 'pn' && res[i].kemasan == 5) {
                        $("#pn5").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    // Paper mint 700
                    else if ((res[i].liqGroup.toLowerCase() == 'pm' || res[i].liqGroup.toLowerCase() == 'pp') && res[i].kemasan == 0.7) {
                        $("#pm7").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    // Paper mint 5000
                    else if ((res[i].liqGroup.toLowerCase() == 'pm' || res[i].liqGroup.toLowerCase() == 'pp') && res[i].kemasan == 5) {
                        $("#pm5").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                    //Black Bull 750
                    else if (res[i].liqGroup.toLowerCase() == 'bb' && res[i].kemasan == 0.75) {
                        $("#BB75").html(res[i].quantity.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                    }
                })
            }
            
           
        }
    })
})

$('#panelRaw').click(function () {
    alkohol()
    gula()
    SW()
    DOP()
    Cukai()
    botolScotch()
    botolVodka()
    botolSpirit()
    botolWhisky()
    botolLiqueurs()
    botolGalon()
    Botol250()
    LabelWhisky()
    LabelBlanco()
    LabelBM()
    Label9()
    LabelLiq()
    BoxScotch()
    BoxWhisky()
    BoxSpirit()
    Box250()
    BoxVodka()
    BoxLiqueurs()



    function alkohol() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=Alkohol',
            success: function (res) {
                let totalAlkohol = 0;
                $.each(res, function (i) {
                    totalAlkohol += res[i].quantity
                    $('#unitAlkohol').html(res[i].unit)
                })
                let round = totalAlkohol.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokAlkohol').html(round)
            }
        })
    }
    function gula() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=Gula',
            success: function (res) {
                let totalGula = 0;
                $.each(res, function (i) {
                    totalGula += res[i].quantity
                    $('#unitGula').html(res[i].unit)
                })
                let round = totalGula.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokGula').html(round)
            }
        })
    }
    function SW() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=ShrinkWrap',
            success: function (res) {
                let totalSW = 0;
                $.each(res, function (i) {
                    totalSW += res[i].quantity
                    $('#unitSW').html(res[i].unit)
                })
                let round = totalSW.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokSW').html(round)
            }
        })
    }
    function DOP() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=DOPGabus',
            success: function (res) {
                let totalDOP = 0;
                $.each(res, function (i) {
                    totalDOP += res[i].quantity
                    $('#unitDOP').html(res[i].unit)
                })
                let round = totalDOP.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokDOP').html(round)
            }
        })
    }
    function Cukai() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getCukai',
            success: function (res) {
                let totalCukai = 0;
                $.each(res, function (i) {
                    totalCukai += res[i].quantity
                    $('#unitCukai').html(res[i].unit)
                })
                let round = totalCukai.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokCukai').html(round)
            }
        })
    }
    function botolScotch() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BotolScotch',
            success: function (res) {
                let totalScotch = 0;
                $.each(res, function (i) {
                    totalScotch += res[i].quantity
                    $('#unitBtlScotch').html(res[i].unit)
                })
                let round = totalScotch.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBtlScotch').html(round)
            }
        })
    }
    function botolVodka() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BotolVodka',
            success: function (res) {
                let totalVodka = 0;
                $.each(res, function (i) {
                    totalVodka += res[i].quantity
                    $('#unit9').html(res[i].unit)
                })
                let round = totalVodka.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#botol9').html(round)
            }
        })
    }
    function botolSpirit() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BotolSpririt',
            success: function (res) {
                let totalSpirit = 0;
                $.each(res, function (i) {
                    totalSpirit += res[i].quantity
                    $('#unitSpirit').html(res[i].unit)
                })
                let round = totalSpirit.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#botolSpirit').html(round)
            }
        })
    }
    function botolWhisky() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BotolWhisky',
            success: function (res) {
                let totalWhisky = 0;
                $.each(res, function (i) {
                    totalWhisky += res[i].quantity
                    $('#unitBtlWhisky').html(res[i].unit)
                })
                let round = totalWhisky.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBtlWhisky').html(round)
            }
        })
    }
    function botolLiqueurs() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BotolLiq',
            success: function (res) {
                let totalLiq = 0;
                $.each(res, function (i) {
                    totalLiq += res[i].quantity
                    $('#unitBtlLiq').html(res[i].unit)
                })
                let round = totalLiq.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBtlLiq').html(round)
            }
        })
    }
    function botolGalon() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BotolGalon',
            success: function (res) {
                let totalGalon = 0;
                $.each(res, function (i) {
                    totalGalon += res[i].quantity
                    $('#unitBtlGalon').html(res[i].unit)
                })
                let round = totalGalon.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBtlGalon').html(round)
            }
        })
    }
    function Botol250() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=Botol250',
            success: function (res) {
                let total250 = 0;
                $.each(res, function (i) {
                    total250 += res[i].quantity
                    $('#unitBtl250').html(res[i].unit)
                })
                let round = total250.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBtl250').html(round)
            }
        })
    }
    function LabelWhisky() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=LabelWhisky',
            success: function (res) {
                let totalWhisky = 0;
                $.each(res, function (i) {
                    totalWhisky += res[i].quantity
                    $('#unitLblWhisky').html(res[i].unit)
                })
                let round = totalWhisky.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokLblWhisky').html(round)
            }
        })
    }
    function LabelBlanco() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=LabelBlanco',
            success: function (res) {
                let totalLabel = 0;
                $.each(res, function (i) {
                    totalLabel += res[i].quantity
                    $('#unitLblBlanco').html(res[i].unit)
                })
                let round = totalLabel.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokLblBlanco').html(round)
            }
        })
    }
    function LabelBM() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=LabelBM',
            success: function (res) {
                let totalLabel = 0;
                $.each(res, function (i) {
                    totalLabel += res[i].quantity
                    $('#unitLblBM').html(res[i].unit)
                })
                let round = totalLabel.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokLblBM').html(round)
            }
        })
    }
    function Label9() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=LabelVodka',
            success: function (res) {
                let totalLabel = 0;
                $.each(res, function (i) {
                    totalLabel += res[i].quantity
                    $('#unitLbl9').html(res[i].unit)
                })
                let round = totalLabel.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokLbl9').html(round)
            }
        })
    }
    function LabelLiq() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=LabelLiq',
            success: function (res) {
                let totalLabel = 0;
                $.each(res, function (i) {
                    totalLabel += res[i].quantity
                    $('#unitLblLiq').html(res[i].unit)
                })
                let round = totalLabel.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokLblLiq').html(round)
            }
        })
    }
    function BoxScotch() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BoxScotch',
            success: function (res) {
                let totalBox = 0;
                $.each(res, function (i) {
                    totalBox += res[i].quantity
                    $('#unitBoxScotch').html(res[i].unit)
                })
                let round = totalBox.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBoxScotch').html(round)
            }
        })
    }
    function BoxWhisky() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BoxWhisjy',
            success: function (res) {
                let totalBox = 0;
                $.each(res, function (i) {
                    totalBox += res[i].quantity
                    $('#unitBoxWhisky').html(res[i].unit)
                })
                let round = totalBox.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBoxWhisky').html(round)
            }
        })
    }
    function BoxSpirit() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BoxSpirit',
            success: function (res) {
                let totalBox = 0;
                $.each(res, function (i) {
                    totalBox += res[i].quantity
                    $('#unitBoxSpirit').html(res[i].unit)
                })
                let round = totalBox.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBoxSpirit').html(round)
            }
        })
    }
    function Box250() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=Box250',
            success: function (res) {
                let totalBox = 0;
                $.each(res, function (i) {
                    totalBox += res[i].quantity
                    $('#unitBox250').html(res[i].unit)
                })
                let round = totalBox.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBox250').html(round)
            }
        })
    }
    function BoxVodka() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BoxVodka',
            success: function (res) {
                let totalBox = 0;
                $.each(res, function (i) {
                    totalBox += res[i].quantity
                    $('#unitBoxVodka').html(res[i].unit)
                })
                let round = totalBox.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBoxVodka').html(round)
            }
        })
    }
    function BoxLiqueurs() {
        $.ajax({
            type: 'GET',
            url: alamat + '/getStock?productGroup=BoxLiqueurs',
            success: function (res) {
                let totalBox = 0;
                $.each(res, function (i) {
                    totalBox += res[i].quantity
                    $('#unitBoxLiqueurs').html(res[i].unit)
                })
                let round = totalBox.toFixed(2)
                round = round.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
                $('#stokBoxLiqueurs').html(round)
            }
        })
    }
})

//function untuk klik button di RAW
$('#btnAlkohol').click(function () {
    window.open(baseurl + '/' + tenant +'/DashboardDetail/stock?productGroup=Alkohol')
})
$('#btnGula').click(function () {
    //window.open(baseurl+'/'+tenant+'/DashboardDetail/Gula')
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=Gula')
})
$('#btnSW').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=ShrinkWrap')
})
$('#btnDOP').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=DOPGabus')
})
$('#btnCukai').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/Cukai')
})
$('#btnBtlScotch').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BotolScotch')
})
$('#btnBtl9').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BotolVodka')
})
$('#btnBtlSpirit').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BotolSpririt')
})
$('#btnBtlWhisky').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BotolWhisky')
})
$('#btnBtlLiq').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BotolLiq')
})
$('#btnBtlGalon').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BotolGalon')
})
$('#btnBtl250').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=Botol250')
})
$('#btnLblWhisky').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=LabelWhisky')
})
$('#btnLblBlanco').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=LabelBlanco')
})
$('#btnLblBM').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=LabelBM')
})
$('#btnLbl9').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=LabelVodka')
})
$('#btnLblLiq').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=LabelLiq')
})
$('#btnBoxScotch').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BoxScotch')
})
$('#btnBoxWhisky').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BoxWhisjy')
})
$('#btnBoxSpirit').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BoxSpirit')
})
$('#btnBox250').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=Box250')
})
$('#btnBoxVodka').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BoxVodka')
})
$('#btnBoxLiqueurs').click(function () {
    window.open(baseurl + '/' + tenant + '/DashboardDetail/stock?productGroup=BoxLiqueurs')
})

