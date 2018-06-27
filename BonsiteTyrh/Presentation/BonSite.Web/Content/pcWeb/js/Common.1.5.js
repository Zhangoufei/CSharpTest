/**
 * Created by lenvo on 2017/9/12.
 */
swiperpic = null;
$(function () {
    /*头部时间*/
    function showTime() {
        var show_day = new Array('星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六');
        var time = new Date();
        var year = time.getFullYear();
        var month = time.getMonth();
        var date = time.getDate();
        var day = time.getDay();
        //var hour=time.getHours();
        //var minutes=time.getMinutes();
        //var second=time.getSeconds();
        //month<10?month='0'+month:month;
        month = month + 1;
        //hour<10?hour='0'+hour:hour;
        //minutes<10?minutes='0'+minutes:minutes;
        //second<10?second='0'+second:second;
        var now_time = '' + year + '年' + month + '月' + date + '日' + ' ' + show_day[day];
        $('.header-time').html(now_time);
    }
    showTime()
    var sWeek = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    var dNow = new Date();
    var CalendarData = new Array(100); var madd = new Array(12); var tgString = "甲乙丙丁戊己庚辛壬癸"; var dzString = "子丑寅卯辰巳午未申酉戌亥"; var numString = "一二三四五六七八九十"; var monString = "正二三四五六七八九十冬腊"; var weekString = "日一二三四五六"; var sx = "鼠牛虎兔龙蛇马羊猴鸡狗猪"; var cYear, cMonth, cDay, TheDate;
    CalendarData = new Array(0xA4B, 0x5164B, 0x6A5, 0x6D4, 0x415B5, 0x2B6, 0x957, 0x2092F, 0x497, 0x60C96, 0xD4A, 0xEA5, 0x50DA9, 0x5AD, 0x2B6, 0x3126E, 0x92E, 0x7192D, 0xC95, 0xD4A, 0x61B4A, 0xB55, 0x56A, 0x4155B, 0x25D, 0x92D, 0x2192B, 0xA95, 0x71695, 0x6CA, 0xB55, 0x50AB5, 0x4DA, 0xA5B, 0x30A57, 0x52B, 0x8152A, 0xE95, 0x6AA, 0x615AA, 0xAB5, 0x4B6, 0x414AE, 0xA57, 0x526, 0x31D26, 0xD95, 0x70B55, 0x56A, 0x96D, 0x5095D, 0x4AD, 0xA4D, 0x41A4D, 0xD25, 0x81AA5, 0xB54, 0xB6A, 0x612DA, 0x95B,
        0x49B, 0x41497, 0xA4B, 0xA164B, 0x6A5, 0x6D4, 0x615B4, 0xAB6, 0x957, 0x5092F,
        0x497, 0x64B, 0x30D4A, 0xEA5, 0x80D65, 0x5AC, 0xAB6, 0x5126D, 0x92E, 0xC96, 0x41A95, 0xD4A, 0xDA5, 0x20B55, 0x56A, 0x7155B, 0x25D, 0x92D, 0x5192B, 0xA95, 0xB4A, 0x416AA, 0xAD5, 0x90AB5, 0x4BA, 0xA5B, 0x60A57, 0x52B, 0xA93, 0x40E95);
    madd[0] = 0; madd[1] = 31; madd[2] = 59; madd[3] = 90;
    madd[4] = 120; madd[5] = 151; madd[6] = 181; madd[7] = 212;
    madd[8] = 243; madd[9] = 273; madd[10] = 304; madd[11] = 334;
    function GetBit(m, n) { return (m >> n) & 1; }
    function e2c() {
        TheDate = (arguments.length != 3) ? new Date() : new Date(arguments[0], arguments[1], arguments[2]);
        var total, m, n, k;
        var isEnd = false;
        var tmp = TheDate.getFullYear();
        total = (tmp - 1921) * 365 + Math.floor((tmp - 1921) / 4) + madd[TheDate.getMonth()] + TheDate.getDate() - 38; if (TheDate.getYear() % 4 == 0 && TheDate.getMonth() > 1) { total++; } for (m = 0; ; m++) { k = (CalendarData[m] < 0xfff) ? 11 : 12; for (n = k; n >= 0; n--) { if (total <= 29 + GetBit(CalendarData[m], n)) { isEnd = true; break; } total = total - 29 - GetBit(CalendarData[m], n); } if (isEnd) break; } cYear = 1921 + m; cMonth = k - n + 1; cDay = total; if (k == 12) { if (cMonth == Math.floor(CalendarData[m] / 0x10000) + 1) { cMonth = 1 - cMonth; } if (cMonth > Math.floor(CalendarData[m] / 0x10000) + 1) { cMonth--; } }
    }
    function GetcDateString() {
        var tmp = ""; tmp += tgString.charAt((cYear - 4) % 10);
        tmp += dzString.charAt((cYear - 4) % 12);
        tmp += "年 ";
        if (cMonth < 1) { tmp += "(闰)"; tmp += monString.charAt(-cMonth - 1); } else { tmp += monString.charAt(cMonth - 1); } tmp += "月"; tmp += (cDay < 11) ? "初" : ((cDay < 20) ? "十" : ((cDay < 30) ? "廿" : "三十"));
        if (cDay % 10 != 0 || cDay == 10) { tmp += numString.charAt((cDay - 1) % 10); } return tmp;
    }
    function GetLunarDay(solarYear, solarMonth, solarDay) {
        if (solarYear < 1921 || solarYear > 2020) {
            return "";
        } else { solarMonth = (parseInt(solarMonth) > 0) ? (solarMonth - 1) : 11; e2c(solarYear, solarMonth, solarDay); return GetcDateString(); }
    }
    var D = new Date();
    var yy = D.getFullYear();
    var mm = D.getMonth() + 1;
    var dd = D.getDate();
    var ww = D.getDay();
    var ss = parseInt(D.getTime() / 1000);
    function getFullYear(D) {// 修正firefox下year错误
        yr = D.getYear(); if (yr < 1000)
            yr += 1900; return yr;
    }
    function showDate() {
        var sValue = GetLunarDay(yy, mm, dd);
        $('.header-time2').html(sValue);
    };
    showDate()
    /*头部导航栏*/
    $('.index-nav-item').hover(function () {
        $(this).addClass('active');

        $(this).find('.index-nav-tit-list').show();
    }, function () {
        $('.index-nav-item').removeClass('active');
        $('.index-nav-tit-list').hide();

    })



    ///*图片轮播*/
    //var swiper = new Swiper('#banner', {
    //    effect: 'fade',
    //    loop: true,
    //    pagination: '.swiper-pagination',
    //    nextButton: '.swiper-button-next',
    //    prevButton: '.swiper-button-prev',
    //    paginationClickable: true,
    //    spaceBetween: 30,
    //    centeredSlides: true,
    //    autoplay: 2500,
    //    autoplayDisableOnInteraction: false,
    //    simulateTouch: false,
    //});
    //$('#banner').hover(function () {
    //    $('.swiper-button-next').show();
    //    $('.swiper-button-prev').show();
    //    $('.swiper-slide-tit').show()

    //}, function () {
    //    $('.swiper-button-next').hide();
    //    $('.swiper-button-prev').hide();
    //    $('.swiper-slide-tit').hide()

    //});
    $(function () {
        var x = 0;
        var timer1 = null;
        $('.left').click(function () {
            clearInterval(timer1);
            x--;
            if (x < 0) {
                x = $('.con img').length - 1;
            };
            bian();
            autoMove();
        });
        $('.right').click(function () {
            clearInterval(timer1);
            x++;
            if (x >= $('.con img').length) {
                x = 0;
            };
            bian();
            autoMove();
        });
        $('.nav li').click(function () {
            clearInterval(timer1);
            x = $('.nav li').index(this);
            bian();
            autoMove();
        })
        function autoMove() {
            timer1 = setInterval(function () {
                x++;
                if (x >= $('.con img').length) {
                    x = 0;
                };
                //alert(x)
                bian();
            }, 5000);
        }
        $(".banner").mouseover(function () {
            $('.left').show();
            $('.right').show()
        })
        $(".banner").mouseout(function () {
            $('.left').hide();
            $('.right').hide()
        })
        autoMove();//进入页面执行
        bian()
        function bian() {
            $('.nav li').removeClass('active');
            $('.nav li').eq(x).addClass('active');
            $('.con li').eq(x).show(0, function () {
                $(this).find('img').animate({ marginTop: -440 + 'px' }, 3000)
            }).siblings().hide(0, function () {
                $('.con li').find('img').css({ marginTop: 0 })
            });
        }
    })


    /*专栏tab切换*/
    $('.Column-right-tit li').hover(function () {
        var arr = ['孝子之养也，乐其心，不违其志。', '一切为了人的发展。', '有理想，有道德，有文化，有纪律。', '种德者，养其心']
        var x = $(this).index();
        console.log(x);
        $('.Column-right-tit li p').removeClass('active1');

        $('.Column-right-tit li p').eq(x).addClass('active1');
        $('.Column-right-con .new-box-list').eq(x).show().siblings('.Column-right-con .new-box-list').hide();
        $('.Column-left li').eq(x).show().siblings('.Column-left li').hide()
        $('.Column-left-pic li').eq(x).show().siblings('.Column-left-pic li').hide();

        $('.Column-left-tit1 img').eq(x).show().siblings('.Column-left-tit1 img').hide();
        $('.Column-left-tit-ddcon').text(arr[x])




    });

    swiperpic = new Swiper('.pic-news-img', {
        loop: true,
        pagination: '.swiper-pagination',
        paginationClickable: true,
        spaceBetween: 30,
        centeredSlides: true,
        autoplay: 2500,
        autoplayDisableOnInteraction: false,
        loopAdditionalSlides: 2,
        observer: true,
        observeParents: true
    });




    /* 校园风景  名师风采*/
    //TeacherElegant
    var swiper = new Swiper('#TeacherElegant', {
        slidesPerView: 7,
        loop: true,
        nextButton: '.swiper-button-next',
        prevButton: '.swiper-button-prev',
        paginationClickable: true,
        spaceBetween: 30,
        centeredSlides: true,
        autoplay: 2500,
        autoplayDisableOnInteraction: false,
        loopAdditionalSlides: 2,
    });

    //schoolView
    var swiper = new Swiper('#schoolView', {
        nextButton: '.swiper-button-next',
        prevButton: '.swiper-button-prev',
        slidesPerView: 5,
        paginationClickable: false,
        spaceBetween: 30,
        loop: true,
        loopAdditionalSlides: 2,
        centeredSlides: true,
        autoplay: 3000,
        autoplayDisableOnInteraction: false
    });

})


//2018-01-26
$(function () {
    $(".new-box-tit1").click(function (event) {
        /* Act on the event */
        var x = $(this).index();
        $(".new-box-tit1").eq(x).css({ color: "#8f000b" }).siblings('.new-box-tit1').css({ color: "#a2a1a1" });
        $(".new-box-tit1").eq(x).addClass('onPic').siblings('.new-box-tit1').removeClass('onPic');
        $(".picNews").eq(x).show().siblings('.picNews').hide();

        //if (x == 0) {
        //    //expression
        //    //news
        //    $("#NewsList .more").attr("href", "/ListPc/228-1.html");
        //} else {

        //    $("#NewsList .more").attr("href", "/ListPc/228-2.html");
        //}




    });


    $("#newsList .new-box-tit5").mouseover(function () {

        var x = $(this).index();

        //$("#newsList .new-box-tit5").eq(x).addClass("onFocus").siblings("#newsList .new-box-tit5").removeClass("onFocus");
        $(".news11").eq(x).show().siblings(".news11").hide();
        if (x == 1) {
            $(".new-box-tit_b p a")[0].style.color = '#ffffff';
            $(".new-box-tit_a p a")[0].style.color = '#c9a3a3';
            $(".new-box-tit_a p i").removeClass('icon-news');
            $(".new-box-tit_a p i").addClass('icon-newsb');
            $(".new-box-tit_b p i").removeClass('icon-con');
            $(".new-box-tit_b p i").addClass('icon-conb');
            swiperpic.stopAutoplay();//$("#news1 .pic-news .pic-news-img .swiper-wrapper").eq(0).hide();
        }
        else {
            $(".new-box-tit_b p a")[0].style.color = '#c9a3a3';
            $(".new-box-tit_a p a")[0].style.color = '#ffffff';
            $(".new-box-tit_a p i").addClass('icon-news');
            $(".new-box-tit_a p i").removeClass('icon-newsb');
            $(".new-box-tit_b p i").addClass('icon-con');
            $(".new-box-tit_b p i").removeClass('icon-conb');
            swiperpic.startAutoplay();//$("#news1 .pic-news .pic-news-img .swiper-wrapper").eq(0).show();
        }

    })









})








