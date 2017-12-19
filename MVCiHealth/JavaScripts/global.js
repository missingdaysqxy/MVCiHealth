$(document).ready(function () {
    $(".rating").rating('refresh', {
        max: 5,
        step: 0.1,
        showClear: false,
    });
    MessageBox('内容');

});

/**
 * 判断所给参数是否为空（包括null或者undefined或者空字符串）
 * @param {any} param
 */
function isNull(param) {
    return (param == "" || param == undefined || param == null) ? true : false;
}
/**
 * 判断所给参数是否不为空（包括null或者undefined或者空字符串）
 * @param {any} param
 */
function isNotNull(param) {
    return (param == "" || param == undefined || param == null) ? false : true;
}

/**
 * 获得指定表单里的所有值
 * @param {HTMLFormElement} $form
 */
function fetchForm(form) {
    var eles = form[0].elements;
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

/**
 * 根据元素name属性填充表单
 * @param {string|JSON|any} data
 * @param {HTMLFormElement} form
 */
function fillForm(data, form) {
    for (var name in data) {
        $('[name="{0}"]'.format(name), form).val(data[name]);
    }
}

/**
 * 在当前页面上弹出一个消息框
 * @param {string|Element|HTMLCollection} title 消息框的标题
 * @param {string|Element|HTMLCollection} content 消息框的内容
 * @param {string|Element|HTMLCollection} footer 消息框的结尾
 */
function MessageBox(content = null, title = null, footer = null) {
    var msg = '<div class="modal fade" id="_msgbox_" tabindex="- 1" role="dialog" aria-labelledby="_msgbox_" aria-describedby="MessageBoxLanchedByServer">'
        + '<div class="modal-dialog modal-sm" role= "document">'
        + '<div class="modal-content">';
    if (isNotNull(title))//标题
        msg += '<div class="modal-header">'
            + '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>'
            + '<h4 class="modal-title">' + title + '</h4>'
            + '</div>';
    if (isNotNull(content))//内容
        msg += '<div class="modal-body">' + content + '</div>';
    if (isNotNull(footer))//结尾
        msg += '<div class="modal-footer">'
            + footer
            + '<button type="button" class="btn btn-default" data-dismiss="modal">确定</button>'
            + '</div>';
    msg += '</div></div></div>';
    $('body #_msgbox_').remove();
    $('body').append(msg);
    $('#_msgbox_').modal({ keyboard: true });
}