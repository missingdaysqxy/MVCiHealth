$(document).ready(function () {
    $(".rating").rating('refresh',{
        max: 5,
        step: 0.1,
        showClear: false,
    });
});