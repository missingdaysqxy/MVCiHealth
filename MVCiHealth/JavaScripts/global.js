$(document).ready(function () {
    $(".rating").rating('refresh',{
        max: 5,
        step: 0.1,
        showClear: false,
    });
});

/**
 * 获得指定表单里的所有值
 * @param $form
 */
function fetchForm(formJqObj) {
    var eles = formJqObj[0].elements;
    var data = {};
    $.each(eles, function (i, v) {
        var target = $(v);
        var name = target.attr('name');
        if (name) {
            if (target[0].tagName == 'SELECT') {
                var cdata = data[name];
                if (typeof cdata == 'object') {
                    cdata.push(target.val());
                } else {
                    if (cdata != undefined) {
                        cdata = data[name] = [cdata];
                        cdata.push(target.val());
                    } else {
                        data[name] = target.val();
                    }
                }
                return;
            }
            var type = target.attr('type');
            if (target[0].tagName == 'TEXTAREA' || 'text,password,hidden,time,date'.indexOf(type = type.toLowerCase()) > -1) {
                var cdata = data[name];
                if (typeof cdata == 'object') {
                    cdata.push(target.val());
                } else {
                    if (cdata != undefined) {
                        cdata = data[name] = [cdata];
                        cdata.push(target.val());
                    } else {
                        data[name] = target.val();
                    }
                }
            } else if (type == 'radio') {
                if (target[0].checked) {
                    data[name] = target.val();
                }
            } else if (type == 'checkbox') {
                if (target[0].checked) {
                    var cdata = data[name];
                    if (!cdata) {
                        cdata = data[name] = [];
                    }
                    cdata.push(target.val());
                }
            }
        }
    });
    return data;
}

function fillForm(data, formJqObj) {
    for (var name in data) {
        $('[name="{0}"]'.format(name), formJqObj).val(data[name]);
    }
}
