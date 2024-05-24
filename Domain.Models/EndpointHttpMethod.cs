namespace InnoClinic.Shared.Domain.Models;

[Flags]
public enum EndpointHttpMethods {
    None    = 0x0,
    Get     = 0x1,
    Post    = 0x2,
    Put     = 0x4,
    Patch   = 0x8,
    Delete  = 0x10,
}