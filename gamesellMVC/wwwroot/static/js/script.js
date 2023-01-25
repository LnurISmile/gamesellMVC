window.onload = function () {

  if (document.querySelector('[id="xbox-start"]')) {
    datecalc(1, "current-lastday", "currency-xbox-start");
  }
  if (document.querySelector('[id="xbox-buy"]')) {
    datecalcxbox(1, "currency-xbox-start");
  }

  

};

function datecalc(monthnumber, classname, currensy) {
  var currencytaker = document.getElementById(currensy);
  var cur_number = currencytaker.dataset.currency;
  var cur_curcalc = currencytaker.dataset.curcalc;
  var cur_name = currencytaker.dataset.currency_name;
  var cur_dayprice = currencytaker.dataset.day_price;
  

  var xbox_month = monthnumber;
  var current_date = new Date();
  var xmonth = current_date.getMonth() + 1;
  var xday = current_date.getDate();
  var xyear = current_date.getFullYear();

  var totalday = 0;

  for (let i = 0; i < xbox_month; i++) {
    var calcday = new Date(xyear, xmonth + i, 0).getDate();
    totalday += calcday;
  }
  if (cur_curcalc == "/") {
    var caculated = (((totalday - xday + 1) * cur_dayprice) / cur_number).toFixed(2);
  } else if (cur_curcalc == "*") {
    var caculated = (((totalday - xday + 1) * cur_dayprice) * cur_number).toFixed(2);
  }

  document.querySelector("." + classname).innerText = caculated +  " " + cur_name;
}

function datecalcxbox(monthnumber, currensy) {
  var currencytaker = document.getElementById(currensy);
  var cur_number = currencytaker.dataset.currency;
  var cur_curcalc = currencytaker.dataset.curcalc;
  var cur_name = currencytaker.dataset.currency_name;
  var cur_dayprice = currencytaker.dataset.day_price;
  var xbox_day = currencytaker.dataset.xboxday;
  var xbox_monthS = currencytaker.dataset.xboxmonth;
  var xbox_year = currencytaker.dataset.xboxyear;


  var xcc_month = document.getElementById('xcc-month');
  var xcc_day = document.getElementById('xcc-day');
  var xcc_price = document.getElementById('xcc-price');
  var xcc_number = document.getElementById('xcc-number');



  var xbox_month = monthnumber;
  
  var current_date = new Date(parseInt(xbox_year),parseInt(xbox_monthS),parseInt(xbox_day));
  console.log(current_date);
  var xmonth = current_date.getMonth() ;
  var xday = current_date.getDate();
  var xyear = current_date.getFullYear();

  var totalday = 0;
  
  

  for (let i = 0; i < xbox_month; i++) {
    var calcday = new Date(xyear, xmonth + i, 0).getDate();
    totalday += calcday;
  }
  if (cur_curcalc == "/") {
    var caculated = (((totalday - xday + 1) * cur_dayprice) / cur_number).toFixed(2);
  } else if (cur_curcalc == "*") {
    var caculated = (((totalday - xday + 1) * cur_dayprice) * cur_number).toFixed(2);
  }


  xcc_number.innerText = monthnumber;
  xcc_month.innerText = monthnumber ;
  xcc_day.innerText = totalday - xday + 1 ;
  xcc_price.innerText = caculated + " " + cur_name;
}

function xcc_change(xccedit){
  var sub_accept = document.getElementById('sub_accept').value;
  var sub_accept1 = parseInt(sub_accept) ;
  // console.log(sub_accept1)
  var xcc_number = document.getElementById("xcc-number");
  var xcc_number1 = xcc_number.innerText;
  var currencytaker = document.getElementById('currency-xbox-start');
  var xboxdate = currencytaker.dataset.xboxdate;
  var current_date = new Date(xboxdate);
  var xmonth = current_date.getMonth() + 1;
  var lastmonth = 12 - xmonth;
  
  if(xccedit == 1){
    if(parseInt(xcc_number1) == 1){

    }else{
      xcc_number.innerHTML = parseInt(xcc_number1) - 1;
      datecalcxbox(xcc_number.innerHTML, "currency-xbox-start");
      document.getElementById('sub_accept').value = parseInt(sub_accept) - 1;
    }
    // console.log(xcc_number)
  }else if(xccedit == 2){
      if(parseInt(xcc_number1) < lastmonth){
        xcc_number.innerHTML = parseInt(xcc_number1) + 1;
     
        datecalcxbox(xcc_number.innerHTML, "currency-xbox-start");
        document.getElementById('sub_accept').value = parseInt(sub_accept) + 1;
      }else{
        
      }
  }

}

function xcc_sub_accept(){
  document.querySelector('.accept-content').classList.remove('xcc-disable');
  document.querySelector('.xcc-buy-btn').classList.add('xcc-disable');

}
function xcc_sub_cancel(){
  document.querySelector('.xcc-buy-btn').classList.remove('xcc-disable');
  document.querySelector('.accept-content').classList.add('xcc-disable');
  

}

function hexToString(hex) {
  if (hex.length % 2 !== 0) {
    hex = '0' + hex;
  }
  var bytes = [];
  for (var n = 0; n < hex.length; n += 2) {
    var code = parseInt(hex.substr(n, 2), 16)
    bytes.push(code);
  }
  return bytes;
}



$('#btn_sendPayment').click(function () {

  $.ajax({
      url: '/Account/Payment?jsonstring=' + $('#jsonstringPayment').val(),
      type: "GET",

     /* data: JSON.stringify($('#jsonstringPayment').val()),*/

      success: function (response) {
          console.log(response);
      },
      error: function (err) {
          console.log(err.statusText);
      }
  });
});

$('.slick-home-slider').slick({
  dots: false,
  infinite: true,
  slidesToShow: 1,
  slidesToScroll: 1,
  fade: true,
  speed: 3000,
  autoplay: true,
  autoplaySpeed: 9000,
  pauseOnHover: false,
  prevArrow: $('.home-slider-bg-prev'),
  nextArrow: $('.home-slider-bg-next'),
  cssEase: 'cubic-bezier(.84,.3,.34,1.33)',
});



$('.home-xbox-slider').slick({
  dots: false,
  infinite: true,
  slidesToShow: 1,
  slidesToScroll: 1,
  fade: true,
  speed: 2000,
  autoplay: true,
  autoplaySpeed: 8000,
  pauseOnHover: false,
  prevArrow: $('.xbox-slider-prev'),
  nextArrow: $('.xbox-slider-next'),
  // cssEase: 'cubic-bezier(.13,.07,.27,1)',
});


$('.more-some-slider').slick({
  dots: false,
  infinite: true,
  slidesToShow: 1,
  slidesToScroll: 1,
  fade: true,
  speed: 2200,
  autoplay: true,
  autoplaySpeed: 10000,
  pauseOnHover: false,
  prevArrow: $('.more-some-slider-prev'),
  nextArrow: $('.more-some-slider-next'),
  cssEase: 'cubic-bezier(.13,.07,.27,1)',
});

$('.slick-postin-slider').slick({
  dots: false,
  infinite: true,
  slidesToShow: 1,
  slidesToScroll: 1,
  fade: false,
  speed: 1000,
  autoplay: false,
  pauseOnHover: false,
  prevArrow: $('.postin-slider-prev'),
  nextArrow: $('.postin-slider-next'),
});

$('.gsic-slider-content').slick({
  dots: false,
  infinite: true,
  slidesToShow: 1,
  slidesToScroll: 1,
  fade: false,
  speed: 1000,
  autoplay: false,
  pauseOnHover: false,
  prevArrow: $('.gsic-slider-arrows-left'),
  nextArrow: $('.gsic-slider-arrows-right'),
});

$('.slick-sale-slider').slick({
  dots: false,
  infinite: true,
  slidesToShow: 7,
  slidesToScroll: 4,
  speed: 700,
  prevArrow: $('.sale-slider-prev'),
  nextArrow: $('.sale-slider-next'),
  responsive: [{
      breakpoint: 1024,
      settings: {
        slidesToShow: 6,
        slidesToScroll: 6,
      }
    },
    {
      breakpoint: 900,
      settings: {
        slidesToShow: 5,
        slidesToScroll: 5,
      }
    },
    {
      breakpoint: 700,
      settings: {
        slidesToShow: 4,
        slidesToScroll: 4
      }
    },
    {
      breakpoint: 600,
      settings: {
        slidesToShow: 3,
        slidesToScroll: 3
      }
    },
    {
      breakpoint: 480,
      settings: {
        slidesToShow: 2,
        slidesToScroll: 2
      }
    }
  ]

});

$('.header-slider').slick({
  dots: false,
  nav: false,
  arrows: true,
  draggable: false,
  infinite: true,
  speed: 3000,
  slidesToShow: 1,
  fade: true,
  // autoplay:true,
  // slidesToScroll: 1,
  // autoplaySpeed: 3000,
  cssEase: 'cubic-bezier(0.7, 0, 0.3, 1)',
  touchThreshold: 100,
  prevArrow: $('.header-slider-prev'),
  nextArrow: $('.header-slider-next'),
}).slickAnimation();

// color picker for selling item background 
function colorpicker() {

  var img = document.getElementById('post-in');

  var vibrant = new Vibrant(img);
  var swatches = vibrant.swatches();
  var colorvibrants = swatches.Vibrant.getRgb();

  const timer = ms => new Promise(res => setTimeout(res, ms))
  async function load() {
    for (var io = 10; io <= 95; io++) {
      document.getElementById('post-in-tab').style.background = 'linear-gradient' + '(' + '180deg, rgba(' + colorvibrants[0] + ',' + colorvibrants[1] + ',' + colorvibrants[2] + ',0.' + io + ')' +
        '0%, rgba(0, 0, 0, 0) 100%)';


      await timer(15);
    }
  }

  load();

}

// colorpicker();



// header tiny menu open 

function openmobilesearch() {
  var overlay = document.querySelector('.header-overlay-remove-tag');
  var threedote = document.querySelector('.menu-items-more-func');
  var language = document.querySelector('.language-content');
  var menu = document.querySelector('.header-profile-menu');
  var cart = document.querySelector('.cart-content');
  var search = document.querySelector('.search-mobile-content');


  if(cart == null){
    
  }else{
    if (cart.classList.contains('pp_block') == true) {
      cart.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }
  

  if (language.classList.contains('pp_block') == true) {
    language.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }
  
  if(menu == null){
    
  }else{
    if (menu.classList.contains('pp_block') == true) {
      menu.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }
  if (threedote.classList.contains('pp_block') == true) {
    threedote.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }



  search.classList.toggle('pp_block');
}

function overlay_remove() {
  var overlay = document.querySelector('.header-overlay-remove-tag');
  var threedote = document.querySelector('.menu-items-more-func');
  var language = document.querySelector('.language-content');
  var menu = document.querySelector('.header-profile-menu');
  var cart = document.querySelector('.cart-content');
  var search = document.querySelector('.search-mobile-content');


  if (overlay.classList.contains('hort-active') == true) {
    overlay.classList.remove('hort-active');
    threedote.classList.remove('pp_block');
    language.classList.remove('pp_block');
    if(menu == null){
    }else{
      menu.classList.remove('pp_block');
    }
    if(cart == null){
    }else{
      cart.classList.remove('pp_block');
    }
    search.classList.remove('pp_block');
  }
}





function openmenumore() {
  var threedote = document.querySelector('.menu-items-more-func');
  var language = document.querySelector('.language-content');
  var menu = document.querySelector('.header-profile-menu');
  var cart = document.querySelector('.cart-content');
  var overlay = document.querySelector('.header-overlay-remove-tag');
  var search = document.querySelector('.search-mobile-content');
  var headerlogin = document.querySelector('.header-login-section');
  if(cart == null){
  }else{
    if (cart.classList.contains('pp_block') == true) {
      cart.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }

  if (language.classList.contains('pp_block') == true) {
    language.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }
  if(menu == null){
    
  }else{
    if (menu.classList.contains('pp_block') == true) {
      menu.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }
 
  if (search.classList.contains('pp_block') == true) {
    search.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }

  threedote.classList.toggle('pp_block');
  overlay.classList.toggle('hort-active');
};

function openlanguagemore() {
  var threedote = document.querySelector('.menu-items-more-func');
  var language = document.querySelector('.language-content');
  var menu = document.querySelector('.header-profile-menu');
  var cart = document.querySelector('.cart-content');
  var overlay = document.querySelector('.header-overlay-remove-tag');
  var search = document.querySelector('.search-mobile-content');

  if(cart == null){
    
  }else{
    if (cart.classList.contains('pp_block') == true) {
      cart.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }

  if (threedote.classList.contains('pp_block') == true) {
    threedote.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }
  if(menu == null){
    
  }else{
    if (menu.classList.contains('pp_block') == true) {
      menu.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }
 
  if (search.classList.contains('pp_block') == true) {
    search.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }

  language.classList.toggle('pp_block');
  overlay.classList.toggle('hort-active');
};


function openheaderprofile() {
  var threedote = document.querySelector('.menu-items-more-func');
  var language = document.querySelector('.language-content');
  var menu = document.querySelector('.header-profile-menu');
  var cart = document.querySelector('.cart-content');
  var overlay = document.querySelector('.header-overlay-remove-tag');
  var search = document.querySelector('.search-mobile-content');

  if(cart == null){
    
  }else{
    if (cart.classList.contains('pp_block') == true) {
      cart.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }

  if (language.classList.contains('pp_block') == true) {
    language.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }
  if (threedote.classList.contains('pp_block') == true) {
    threedote.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }
  if (search.classList.contains('pp_block') == true) {
    search.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }

  menu.classList.toggle('pp_block');
  overlay.classList.toggle('hort-active');

};

function opencartheader() {
  var threedote = document.querySelector('.menu-items-more-func');
  var language = document.querySelector('.language-content');
  var menu = document.querySelector('.header-profile-menu');
  var cart = document.querySelector('.cart-content');
  var overlay = document.querySelector('.header-overlay-remove-tag');
  var search = document.querySelector('.search-mobile-content');


  if (language.classList.contains('pp_block') == true) {
    language.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }
  if (threedote.classList.contains('pp_block') == true) {
    threedote.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }
  if(menu == null){
    
  }else{
    if (menu.classList.contains('pp_block') == true) {
      menu.classList.remove('pp_block')
      overlay.classList.remove('hort-active');
    }
  }
 
  if (search.classList.contains('pp_block') == true) {
    search.classList.remove('pp_block')
    overlay.classList.remove('hort-active');
  }

  cart.classList.toggle('pp_block');
  overlay.classList.toggle('hort-active');

};

function seeorhide_input(seehidename) {
  var x = document.getElementById(seehidename);
  var see = document.querySelector('.' + seehidename);

  if (x.type === "password") {
    x.type = "text";
    console.log("password idi text oldu")
    see.innerHTML = '<i class="las la-eye"></i>';
  } else {
    x.type = "password";
    console.log("text idi pass oldu")
    see.innerHTML = '<i class="las la-eye-slash"></i>';
  }
};

function checkedif(checkedname) {
  var mark = document.querySelector('.' + checkedname);
  var id = document.getElementById(checkedname);

  if (id.checked == true) {
    id.checked = false
    mark.classList.remove("pp_checkmark_show");

  } else {
    id.checked = true
    mark.classList.add("pp_checkmark_show");
  }
};

// login and register open 
function openlogreg(openname) {
  var i;
  var x = document.getElementsByClassName("overlay-tabs");

  g = document.createElement('div');
  g.setAttribute("class", "overlay_background");
  g.setAttribute("onclick", "logregoverlay_remove()");
  if (document.getElementById("overlay-logreg").querySelectorAll(".overlay_background").length > 0) {} else {
    document.getElementById("overlay-logreg").appendChild(g);
  }
  for (i = 0; i < x.length; i++) {
    x[i].style.right = "-100%";
  }
  document.querySelector('.overlay_background').style.visibility = 'visible';
  document.querySelector('.overlay_background').style.opacity = '1';
  document.getElementById(openname).style.right = "0";
  document.body.style.overflow = "hidden";
};



// login overlay remove 
function logregoverlay_remove() {
  g = document.querySelector(".overlay_background");
  if (document.getElementById("overlay-logreg").querySelectorAll(".overlay_background").length > 0) {
    document.getElementById("overlay-logreg").removeChild(g);
  } else {}
  var i;
  var x = document.getElementsByClassName("overlay-tabs");
  for (i = 0; i < x.length; i++) {
    x[i].style.right = "-100%";
  }
  document.body.style.overflow = "auto";
};

function library_game_open(library_game_id) {
  var maingameid = document.getElementById(library_game_id);
  var mainGameInfoInput = maingameid.querySelector('.library-game-info-input');
  var game_name = mainGameInfoInput.dataset.name;
  var game_image = mainGameInfoInput.dataset.image;
  var game_guide = mainGameInfoInput.dataset.guide;
  var game_platform = mainGameInfoInput.dataset.platform;
  var game_login = mainGameInfoInput.dataset.login;
  var game_password = mainGameInfoInput.dataset.password;

  document.querySelector('.game-library-popup-image img').src = game_image;
  document.querySelector('.game-library-popup-image .game-guide-content a').href = game_guide;
  document.querySelector('.game-library-popup-info h4 span').innerHTML = game_name;
  document.querySelector('.game-library-popup-info h5 span').innerHTML = game_platform;
  document.querySelector('.game-library-popup-login h4 span').innerHTML = game_login;
  document.querySelector('.game-library-popup-login h5 span').innerHTML = game_password;

  document.querySelector('.library-page-overlay').classList.add('library-page-overlay-show');

};

function library_popup_exit() {
  document.querySelector('.library-page-overlay').classList.remove('library-page-overlay-show');
};


function propicchange(){
  document.getElementById('profile_image').click();
}
function loadfile(event) {
  var output = document.getElementById('epis_output');
  output.src = URL.createObjectURL(event.target.files[0]);
  output.onload = function() {
    URL.revokeObjectURL(output.src) // free memory
  }
};

VanillaTilt.init(document.querySelectorAll(".library-game-image"), {
  max: 25,
  perspective: 1000,
  scale: 1,
  speed: 2000,
  easing: "cubic-bezier(.03,.98,.52,.99)"
});

function copyToClipboard(copyid) {
  var textBox = document.getElementById(copyid).innerText;
  navigator.clipboard.writeText(textBox);
};

function showdeletecartitem(event) {

  cart_id = document.querySelector("[data-deleteoverlay='" + event + "']");
  cart_id.classList.add('pp_flex');

};

function hidedeletecartitem(event) {
  cart_id = document.querySelector("[data-deleteoverlay='" + event + "']");
  cart_id.classList.remove('pp_flex');
};

function deletecartitem(event) {
  cart_id = document.querySelector("[data-cart-item-id='" + event + "']");
  cart_id.remove();
  cart_class = document.querySelectorAll('.cart-items');

  if (cart_class == null) {
    console.log("a");
  } else {
    console.log("b");
  }

};

// function card_number_limit(event) {
//   document.getElementById(event).value = document.getElementById(event).value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');
//   document.getElementById(event).maxLength = "16";
// };

function card_pss_limit(event) {
  document.getElementById(event).value = document.getElementById(event).value.replace(/[^0-9]/g, '').replace(/(\..*)\./g, '$1');
  document.getElementById(event).maxLength = "3";
};



function newpasscheck() {
  if (document.getElementById('newpass').value ==
    document.getElementById('newpassconfirm').value && document.getElementById('newpass').value.length > 1) {
    document.getElementById('passwordmatch').style.color = '#00cc96';
    document.getElementById('passwordmatch').innerHTML = 'The passwords are matching';
    document.getElementById('newpassconfirmbtn').disabled = false;
  } else {
    document.getElementById('passwordmatch').style.color = '#cc2900';
    document.getElementById('passwordmatch').innerHTML = "The passwords don't match";
    document.getElementById('newpassconfirmbtn').disabled = true;
  }
}