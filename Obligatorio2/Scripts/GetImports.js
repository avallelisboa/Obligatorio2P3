$(document).ready(() => {
    var list;
    var table = $("#importsList");
    $.get("http://localhost:56488/imports/getImports", (data) => {

        table.html('');
        table.append(` <tr>
                                 <th>Id de Producto</th>
                                 <th>Cantidad</th>
                                 <th>Precio unitario</th>
                                 <th>Rut del cliente</th>
                                 <th>Está almacenado</th>
                               </tr>
                            `);
        data.forEach((i) => {
            table.append(`
                                    <tr>
                                    <td>
                                    ${i.ProductId}
                                    </td>
                                    <td>
                                    ${i.Ammount}
                                    </td>
                                    <td>
                                    ${i.PriceByUnit}
                                    </td>
                                    <td>
                                    ${i.Tin}
                                    </td>
                                    <td>
                                    ${i.IsStored}
                                    </td>
                                </tr>`);
        });
    }, "json");
});