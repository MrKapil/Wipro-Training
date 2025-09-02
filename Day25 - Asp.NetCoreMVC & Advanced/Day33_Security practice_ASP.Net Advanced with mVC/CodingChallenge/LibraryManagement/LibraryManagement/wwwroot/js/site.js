$(function () {
    // open create modal
    $('#btnAddBook').on('click', function () {
        $.get('/Books/Create', function (html) {
            showModal('Add Book', html);
        });
    });

    // delegate edit
    $('#booksList').on('click', '.btn-edit', function () {
        const id = $(this).data('id');
        $.get('/Books/Edit/' + id, function (html) {
            showModal('Edit Book', html);
        });
    });

    // delegate delete
    $('#booksList').on('click', '.btn-delete', function () {
        if (!confirm('Delete this book?')) return;
        const id = $(this).data('id');
        $.post('/Books/Delete', { id: id })
            .done(function (res) {
                alert(res.message);
                refreshList();
            })
            .fail(function () {
                alert('Delete failed');
            });
    });

    // handle form submit (create/edit) - delegate to modal form
    $('#modalPlaceholder').on('submit', '#bookForm', function (e) {
        e.preventDefault();
        var $form = $(this);
        var action = $form.find('input[name="BookId"]').val() && $form.find('input[name="BookId"]').val() > 0 ? '/Books/Edit' : '/Books/Create';
        $.ajax({
            url: action,
            method: 'POST',
            data: $form.serialize()
        }).done(function (res) {
            if (res.success) {
                hideModal();
                refreshList();
                alert(res.message);
            } else {
                // if we received partial HTML (validation errors), replace form
                if (typeof res === 'string') {
                    $('#modalBody').html(res);
                } else {
                    alert('Error: ' + res.message);
                }
            }
        }).fail(function () {
            alert('Server error');
        });
    });

    function refreshList() {
        $.get('/Books/BookListPartial', function (html) {
            $('#booksList').html(html);
        });
    }

    function showModal(title, html) {
        var modalHtml = `
        <div class="modal fade" id="ajaxModal" tabindex="-1" aria-hidden="true">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">${title}</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
              </div>
              <div class="modal-body" id="modalBody">${html}</div>
            </div>
          </div>
        </div>`;
        $('#modalPlaceholder').html(modalHtml);
        var modal = new bootstrap.Modal(document.getElementById('ajaxModal'));
        modal.show();
    }

    function hideModal() {
        var modalEl = document.getElementById('ajaxModal');
        if (modalEl) {
            var modal = bootstrap.Modal.getInstance(modalEl);
            modal.hide();
            setTimeout(function () { $('#modalPlaceholder').empty(); }, 350);
        }
    }
});
