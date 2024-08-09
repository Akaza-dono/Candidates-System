function printCandidatesTable(candidates) {
    console.log(candidates)
    $("#dataGridContainer").dxDataGrid({
        dataSource: candidates,
        columnResizingMode: 'nextColumn',
        columnMinWidth: 50,
        columnAutoWidth: true,
        columns: [
            { dataField: "idCandidate", caption: "ID", allowEditing: false },
            { dataField: "name", caption: "Nombre", validationRules: [{ type: "required", message: "name is required" }] },
            { dataField: "surname", caption: "surname", validationRules: [{ type: "required", message: "surname is required" }] },
            { dataField: "email", caption: "email", validationRules: [{ type: "required", message: "email is required" }] },
            { dataField: "birthdate", caption: "birthdate", dataType: "date", validationRules: [{ type: "required", message: "birthdate is required" }] },
            { dataField: "modifyDate", caption: "modifyDate", allowEditing: false, dataType: "date" },
            { dataField: "insertDate", caption: "insertDate", allowEditing: false, dataType: "date" },
            {
                type: "buttons",
                buttons: ["edit", "delete", {
                    hint: "Mostrar Experiencia",
                    icon: "info",
                    onClick: function (e) {
                        fetchAllExperiencesByCandidates(e.row.data.idCandidate)
                    }
                }]
            }
        ],
        scrolling: {
            rowRenderingMode: 'virtual',
        },
        showBorders: true,
        rowAlternationEnabled: true,
        showColumnLines: false,
        showRowLines: true,
        searchPanel: {
            visible: true,
        },
        paging: {
            pageSize: 15,
        },
        pager: {
            visible: true,
            allowedPageSizes: [10, 'all'],
            showPageSizeSelector: true,
            showInfo: true,
            showNavigationButtons: true,
        },
        editing: {
            mode: "row",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        },
        onRowUpdated(e) {
            console.log(e)
            fetch('/api/Candidates/UpdateCandidate', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(e.data)
            }).then(res => res.json())
                .then(data => {
                    console.log(data)
                    Swal.fire({
                        title: 'Hecho!',
                        text: 'Actualizado Correctamente',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    })
                })

        },
        onRowRemoving(eventArgs) {
            console.log(eventArgs.data.idCandidate);
            fetch(`/api/Candidates/DeleteCandidate/${eventArgs.data.idCandidate}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(data => data.json()).then(res => {
                if (res) {
                    Swal.fire({
                        title: 'Hecho!',
                        text: 'Eliminado Correctamente',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    })
                }
            })
        },
        onRowInserting(e) {
            console.log(e.data)
            fetch('/api/Candidates/CreateCandidate', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(e.data)
            }).then(res => res.json()).then(data => {
                console.log(data);
               
                if (data.details == 'El correo electrónico ya existe.') {

                    Swal.fire({
                        title: 'Error',
                        text: 'El correo electronico ya existe',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    })
                } else {
                    Swal.fire({
                        title: 'Hecho!',
                        text: 'Candidato creado correctamente',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    }).then((result) => {
                        if (result.isConfirmed) {
                            location.reload(); 
                            fetchAllCandidates();
                        }
                    });
                }
            })
        }
    });
};

function fetchAllCandidates() {
    try {
        fetch('./api/Candidates/GetAllCandidates')
            .then(data => data.json())
            .then(res => {
                printCandidatesTable(res)
            })
    } catch (error) {
        console.error('Error al procesar la solicitud:', error);
    }
}
let idCandidate_ = 0;
function fetchAllExperiencesByCandidates(id) {
    document.getElementById('val').value = id;
    try {
        fetch(`./api/Experience/GetExperience/${id}`)
            .then(data => data.json())
            .then(res => {
                printExperienceTable(res)
            })
    } catch (error) {
        console.error('Error al procesar la solicitud:', error);
    }
}


document.getElementById('listar').addEventListener('click', fetchAllCandidates)


function printExperienceTable(data) {
    console.log(data)
    debugger
    $("#dataGridContainerExperience").dxDataGrid({
        dataSource: data,
        columnResizingMode: 'nextColumn',
        columnMinWidth: 50,
        columnAutoWidth: true,
        columns: [
            { dataField: "idCandidateExperience", caption: "idCandidateExperience", allowEditing: false },
            { dataField: "job", caption: "job", validationRules: [{ type: "required", message: "Job is required" }] },
            { dataField: "company", caption: "company", validationRules: [{ type: "required", message: "company is required" }] },
            { dataField: "description", caption: "description", validationRules: [{ type: "required", message: "description is required" }] },
            {
                dataField: "salary", caption: "salary", dataType: "number", validationRules: [
                    { type: "required", message: "Salary is required" }
                ] },
            { dataField: "beginDate", caption: "beginDate", dataType: "date", validationRules: [{ type: "required", message: "beginDate is required" }] },
            { dataField: "endDate", caption: "endDate", dataType: "date", validationRules: [{ type: "required", message: "endDate is required" }] },
            { dataField: "insertDate", caption: "insertDate", allowEditing: false, dataType: "date" },
            { dataField: "modifyDate", caption: "modifyDate", allowEditing: false, dataType: "date" },
            {
                type: "buttons",
                buttons: ["edit", "delete"]
            }
        ],
        scrolling: {
            rowRenderingMode: 'virtual',
        },
        showBorders: true,
        rowAlternationEnabled: true,
        showColumnLines: false,
        showRowLines: true,
        searchPanel: {
            visible: true,
        },
        paging: {
            pageSize: 15,
        },
        pager: {
            visible: true,
            allowedPageSizes: [10, 15, 20, 'all'],
            showPageSizeSelector: true,
            showInfo: true,
            showNavigationButtons: true,
        },
        editing: {
            mode: "row",
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true
        },
        onRowUpdated(e) {
            console.log(e)
            fetch('/api/Experience/UpdateExperience', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(e.data)
            }).then(res => res.json())
                .then(data => {
                    console.log(data)
                    Swal.fire({
                        title: 'Hecho!',
                        text: 'Actualizado Correctamente',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    })
                })

        },
        onRowRemoving(eventArgs) {
            console.log(eventArgs);
            fetch(`/api/Experience/DeleteExperience/${eventArgs.data.idCandidateExperience}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(data => data.json()).then(res => {
                console.log(res)
                if (res) {
                    Swal.fire({
                        title: 'Hecho!',
                        text: 'Eliminado Correctamente',
                        icon: 'success',
                        confirmButtonText: 'OK'
                    })
                }
            })
        },
        onRowInserting(e) {
            let { beginDate, company, description, endDate, insertDate, job, modifyDate, salary } = e.data;
            let obj = {
                idCandidate: document.getElementById('val').value, 
                beginDate,
                company,
                description,
                endDate,
                insertDate,
                job,
                modifyDate,
                salary
            };
            fetch('/api/Experience/CreateExperience', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(obj)
            }).then(res => res.json()).then(data => {
                if (data.details == 'El correo electrónico ya existe.') {
                    fetchAllCandidates()
                    Swal.fire({
                        title: 'Error',
                        text: 'El correo electronico ya existe',
                        icon: 'error',
                        confirmButtonText: 'OK'
                    })
                }
            })
        }
    });
};

