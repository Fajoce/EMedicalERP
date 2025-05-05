
# Requerimientos
Visual Studio 2022 Net Core 8.0
Sql server 20.2

# Paquetes Nuget instalados
Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0"
Microsoft.EntityFrameworkCore" Version="9.0.4" 
Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.4"
Microsoft.IdentityModel.Tokens" Version="8.9.0" 
FluentValidation.AspNetCore" Version="11.3.0"
Microsoft.Extensions.Configuration" Version="9.0.4"

# Tipo de Arquitectura de la aplicacion
- Monolitica por N capas
# Enfoque de diseño
- DDD Domain Driven Design: Diseño basado en el dominio
# Patrones de Diseño
  - Repositorio
  - Specification
  - CQRS
  - Result
# Lamba Functions
- Para enviar un correo y un sms al telefono del paciente cuando reserve la cita
# Principos Solid - Clean Architecture


# Inicio de sesion, si el usuario existe se creara un token que le permitira acceder a los diferentes endpoints de la aplicacion

![image](https://github.com/user-attachments/assets/6d8885ec-db1d-4804-b34d-92192c4de4f7)

# Endpoint que permite verificar la disponibilidad de los servicios ya sean generales o de odontologia
![image](https://github.com/user-attachments/assets/d6365387-7237-4bc9-a638-638f423e3b79)

# Endpoint que permite seleccionar y reservar la cita
![image](https://github.com/user-attachments/assets/901d8beb-df19-4e7b-86fe-be09e6b55120)



