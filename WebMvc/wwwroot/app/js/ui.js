(function (window) {
    function store() {
        var _store = {
            ui: {
                dataTable: {
                    make: function (opts) {
                        opts.selector = opts.selector || '#table';
                        opts.url_list = opts.url_list || '';
                        opts.url_delete = opts.url_delete || '';
                        opts.url_edit = opts.url_edit || '';
                        opts.url_details = opts.url_details || '';
                        
                        opts.dataFilter = opts.dataFilter || function (data) {
                            var json = $.parseJSON(data);
                            json.recordsTotal = json.count;
                            json.recordsFiltered = json.count;
                            return JSON.stringify(json)
                        };

                        opts.autoWidth = opts.autoWidth || false;
                        opts.proccessing = opts.proccessing || true;
                        opts.serverSide = opts.serverSide || true;
                        //opts.useEditButton = opts.useEditButton || true;
                        //opts.useDeleteButton = opts.useDeleteButton || true;
                        opts.columns = opts.columns || [];
                        opts.columnDefs = opts.columnDefs || [];

                        let action = {};
                        let hasAction = opts.hasOwnProperty('action');
                        action['view'] = hasAction && opts.action.hasOwnProperty('view') ? opts.action.view : false;
                        action['edit'] = hasAction && opts.action.hasOwnProperty('edit') ? opts.action.edit : true;
                        action['delete'] = hasAction && opts.action.hasOwnProperty('delete') ? opts.action.delete : true;
                        opts.order = opts.order || ((action['view'] || action['edit'] || action['delete']) ? [[1, "asc"]] : [[0, "asc"]]);

                        if (action['view'] || action['edit'] || action['delete']) {
                            let deleteFuncName = 'store.ui.dataTable.deleteRow';
                            let editFuncName = opts.editCallbackName || 'store.ui.popup.openEditor';
                            let viewFuncName = opts.editCallbackName || 'store.ui.popup.openEditor';
                            opts.columnDefs.push({ className: 'text-center', targets: [0] });
                            opts.columns.unshift({
                                data: null,
                                title: 'Action',
                                orderable: false,
                                width: "100px",
                                render: function (data, type, row, meta) {
                                    let renderButton = '';

                                    if (action['view']) {
                                        let callback = viewFuncName + "({url:'" + opts.url_details + row.id + "' ,actionUrl:'" + opts.url_edit + row.id + "',mode: 'view'})";

                                        renderButton += '<button class="btn btn-primary btn-sm mr-3"'
                                            + ' data-title="View"'
                                            + ' onclick="' + callback + '"'
                                            + ' ><i class="fa fa-book"></i></button>';
                                    }

                                    if (action['edit']) {
                                        let callback = opts.url_edit ? editFuncName + "({url:'" + opts.url_details + row.id + "' ,actionUrl:'" + opts.url_edit + row.id + "',mode: 'edit'})"
                                            : opts.editCallbackName + "(" + row.id + ")"

                                        renderButton += '<button class="btn btn-primary btn-sm mr-3"'
                                            + ' data-title="Edit"'
                                            + ' onclick="' + callback + '"'
                                            + ' ><i class="fa fa-pencil"></i></button>';
                                    }

                                    if (action['delete']) {
                                        let url_delete = opts.url_delete ? "'" + opts.url_delete + row.id + "', '" + opts.selector + "'" : row.id;

                                        renderButton += '<button class="btn btn-secondary btn-sm"'
                                            + ' onclick="' + deleteFuncName + '(' + url_delete + ')"'
                                            + ' ><i class="fa fa-trash"></i></button>';
                                    }
                                    return renderButton;
                                }
                            });
                        }

                        opts.columns.push({ data: null, render: function () { return ''; }, orderable: false });

                        let spinnerCol = opts.columns.length;

                        let $table = $(opts.selector).DataTable({
                            proccessing: opts.proccessing,
                            serverSide: opts.serverSide,
                            lengthChange: false,
                            pageLength: 10,
                            searching: false,
                            order: opts.order,
                            columns: opts.columns,
                            columnDefs: opts.columnDefs,
                            ajax: {
                                url: opts.url_list,
                                dataFilter: opts.dataFilter,
                                data: function (source) {
                                    let pageSize = source.length;
                                    let pageIndex = (source.start + source.length) / source.length;
                                    let sortBy = '';
                                    let sortDir = '';

                                    if (source && source.order && source.order.length) {
                                        let columns = source.columns;
                                        let order = source.order;
                                        let columnIndex = order[0]["column"];

                                        sortBy = columns[columnIndex].data;
                                        sortDir = order[0]["dir"];
                                    }
                                    return { pageSize: pageSize, pageIndex: pageIndex, sortBy: sortBy, sortDir: sortDir };
                                },
                                beforeSend: function () {
                                    $(opts.selector + ' > tbody').html(
                                        '<tr class="odd">' +
                                        '<td valign="top" colspan="' + spinnerCol + '" class="dataTables_empty">' +
                                        '       <div class="container-fluid">' +
                                        '           <div class="row justify-content-center">' +
                                        '               <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>' +
                                        '           </div>' +
                                        '           <div class="row justify-content-center mt-3">' +
                                        '               <span>Loading...</span>' +
                                        '           </div>' +
                                        '       </div>' +
                                        '   </td>' +
                                        '</tr>'
                                    );
                                }
                            }
                        });
                    },
                    deleteRow: function (url_delete, selector) {
                        $.ajax({
                            type: 'DELETE',
                            url: url_delete,
                            error: function (xhr, ex) {
                                alert(xhr.responseText);
                            },
                            success: this.reload(selector)
                        });
                    },
                    reload: function (selector) {
                        $(selector).DataTable().ajax.reload();
                    }
                },
                popup: {
                    makeEditor: function (opts) {
                        opts.selector = opts.selector || 'viewModal';
                        opts.patialViewId = opts.patialViewId || 'partialView';
                        opts.formSelector = opts.formSelector || '#formDetails';
                        $(opts.selector).html(
                            '<div class="modal-dialog modal-dialog-scrollable modal-dialog-centered container-md"' +
                            '     style="max-width: 800px !important;">' +
                            '    <div class="modal-content">' +
                            '        <div class="modal-header">' +
                            '            <h5 class="modal-title">Modal title</h5>' +
                            '            <button type="button" class="close" data-dismiss="modal" aria-label="Close">' +
                            '                <span aria-hidden="true">&times;</span>' +
                            '            </button>' +
                            '        </div>' +
                            '        <div id="' + opts.patialViewId + '" class="modal-body">' +
                            '' +
                            '        </div>' +
                            '        <div class="modal-footer">' +
                            '            <button type="button" class="btn btn-secondary left" data-dismiss="modal">Close</button>' +
                            '            <button id="btnSave" onclick="$(\'' + opts.formSelector + '\').submit()" type="button" class="btn btn-primary right" data-edit-mode="" data-action-url="" onclick="store.ui.data.save({btnSaveSelector: \'#btnSave\'})">Save changes</button>' +
                            '        </div>' +
                            '    </div>' +
                            '</div>'
                        );

                        $(opts.selector).on('hide.bs.modal', function (e) {
                            if ($(opts.formSelector).data("dirty") &&
                                !confirm("Are you sure you want to leave with out saving the data?")) {
                                e.preventDefault();
                            }
                        });
                    },
                    openEditor: function (opts) {
                        opts.mode = opts.mode || '';
                        opts.modalSelector = opts.modalSelector || '#viewModal';
                        opts.partialViewSelector = opts.partialViewSelector || '#partialView';
                        opts.btnSaveSelector = opts.btnSaveSelector || '#btnSave';
                        opts.url = opts.url || '';
                        opts.actionUrl = opts.actionUrl || '';

                        let title = opts.mode == 'edit' ? 'Edit Item' : (opts.mode == 'view' ? 'View Item' : 'New Item');
                        $(opts.modalSelector).modal('show');
                        $(opts.modalSelector + ' .modal-title').text(title);

                        let $view = $(opts.partialViewSelector);
                        let spinner = this.getSpinnerHtml();

                        $view.html(spinner);
                        $view.load(opts.url);

                        let $btnSave = $(opts.btnSaveSelector);
                        $btnSave.data("edit-mode", "edit");
                        $btnSave.prop("disabled", true);
                        $btnSave.data("action-url", opts.actionUrl);
                    },
                    getSpinnerHtml: function () {
                        let html =
                            '<div class="container-fluid">' +
                            '    <div class="row justify-content-center">' +
                            '        <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>' +
                            '    </div>' +
                            '    <div class="row justify-content-center mt-3">' +
                            '        <span>Loading...</span>' +
                            '    </div>' +
                            '</div>';
                        return html;

                    },
                    activateValidator: function (formSelector, btnSaveSelector) {
                        $.validator.unobtrusive.parse(formSelector);

                        let $controls = $(formSelector + " :input");

                        $controls.one("change.checkDirty keypress.checkDirty", function () {
                            let $form = $(this).closest('form');
                            let isDirty = $form.data('dirty');
                            if (isDirty !== true) {
                                $form.data('dirty', true);
                                $(btnSaveSelector).prop("disabled", false);
                                $controls.off("change.checkDirty keypress.checkDirty");
                            }
                        });
                    }
                },
                data: {
                    save: function (opts) {
                        debugger;
                        opts.formObject = opts.formObject || null;
                        opts.formEvent = opts.formEvent || null;
                        if (opts.formEvent) opts.formEvent.preventDefault()

                        opts.formSelector = opts.formSelector || '#formDetails';
                        let $form = $(opts.formSelector);
                        if (!$form.valid()) return;

                        opts.btnSaveSelector = opts.btnSaveSelector || '#btnSave';
                        let $btnSave = $(opts.btnSaveSelector);

                        let url = $btnSave.data('action-url');
                        if (!url) return;

                        opts.modalSelector = opts.modalSelector || '#viewModal';
                        opts.tableSelector = opts.tableSelector || '#table';

                        let json = this.objectifyForm($form.serializeArray());
                        let formData = new FormData(opts.formObject);
                        debugger;
                        $.ajax({
                            type: 'POST',
                            cache: false,
                            processData: false,
                            contentType: false,
                            url: url,
                            data: formData,
                            success: function () {
                                $form.data('dirty', false);
                                $(opts.modalSelector).modal('hide');
                                store.ui.dataTable.reload(opts.tableSelector);
                                instance = null;
                            }
                        });
                        event.preventDefault();
                    },
                    objectifyForm: function (formArray) {
                        //serialize data function
                        var returnArray = {};
                        for (var i = 0; i < formArray.length; i++) {
                            returnArray[formArray[i]['name']] = formArray[i]['value'];
                        }
                        return returnArray;
                    }
                },
                image: {
                    change: function (fileInput, imgSelector) {
                        if (fileInput.files && fileInput.files[0]) {
                            var reader = new FileReader();

                            reader.onload = function (e) {
                                $(imgSelector)
                                    .attr('src', e.target.result);
                                //.width(150)
                                //.height(200);
                            };

                            reader.readAsDataURL(fileInput.files[0]);
                        }
                    }
                },
                basket: {
                    get: function () {
                        let items = {};
                        let basket = localStorage.getItem('basket');
                        if (basket != null) {
                            let data = JSON.parse(basket);
                            items = data.items || {};
                        }
                        return items;
                    },
                    set: function (items) {
                        localStorage.setItem('basket', JSON.stringify({
                            time_val: 1,
                            items: items
                        }));
                        this.updateUI(items);
                    },
                    remove: function () {
                        localStorage.removeItem('basket');
                        this.updateUI();
                    },
                    addItem: function (item) {
                        let items = this.get();
                        let productId = item["id"];
                        let newQuantity = 0;
                        if (items[productId] === undefined || items[productId] === null) {
                            newQuantity = item.quantity;
                            items[productId] = item;
                        } else {
                            newQuantity = items[productId]["quantity"] + item.quantity;
                            items[productId]["quantity"] = newQuantity;
                        }

                        this.set(items);
                        return newQuantity;
                    },
                    deleteItem: function (productId) {
                        let items = this.get();
                        if (items[productId] !== undefined || items[productId] !== null) {
                            delete items[productId];
                        }
                        let num = Object.keys(items).length;
                        if (num > 0) {
                            this.set(items);
                        } else {
                            this.remove();
                        }
                    },
                    count: function () {
                        let items = this.get();
                        return Object.keys(items).length;
                    },
                    updateUI: function (items) {
                        let num = items ? Object.keys(items).length : 0;
                        let $spCountItems = $("#spCountItems");
                        if (num > 0) {
                            $spCountItems.show();
                            $spCountItems.text(num);
                        } else {
                            $spCountItems.hide();
                        }
                    }
                },
                utils: {
                    formatNumber: function (val) {
                        return parseFloat(val).toLocaleString("en-US");
                    },
                    formatCurrency: function (val) {
                        return parseFloat(val).toLocaleString("en-US", { style: 'currency', currency: 'USD' });
                    }
                }
            }
        };
        return _store;
    }

    if (typeof (window.store) === 'undefined') {
        window.store = store();
        store = window.store;
    }
})(window);