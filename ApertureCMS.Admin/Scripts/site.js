﻿ $(function () {

     //Share Code Generator
     $("#generate-code").on("click", function () {
         
         $("#ShareCode").val(makeid());
     });

 });

 $(function () {
     $("#selectable").selectable();
 });



 function makeid() {
     var text = "";
     var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

     for (var i = 0; i < 6; i++)
         text += possible.charAt(Math.floor(Math.random() * possible.length));

     return text;
 }