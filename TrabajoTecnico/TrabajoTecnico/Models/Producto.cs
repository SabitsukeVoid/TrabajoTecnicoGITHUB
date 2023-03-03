using System;
using System.Collections.Generic;

namespace TrabajoTecnico.Models;

public partial class Producto
{
    public string IdProducto { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Precio { get; set; }

    public virtual ICollection<DetalleProducto> DetalleProductos { get; } = new List<DetalleProducto>();
}
