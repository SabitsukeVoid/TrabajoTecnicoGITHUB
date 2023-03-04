using System;
using System.Collections.Generic;

namespace TrabajoTecnico.Models;

public partial class DetalleProducto
{
    public string IdDetalleProducto { get; set; } = null!;

    public string? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public int? ValorTotal { get; set; }

    public int? ValorIva { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

}

