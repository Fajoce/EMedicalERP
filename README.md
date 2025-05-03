
# Requerimientos
Net Core 8.0
Sql server 2024

# Paquetes Nuget instalados
 <ItemGroup>
   <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
   <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.4" />
   <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.4" />
   <PackageReference Include="Microsoft.IdentityModel.Tokens" Version="8.9.0" />
 </ItemGroup>


#Inicio de sesion, si el usuario existe se creara un token que le permitira acceder a los diferentes endpoints d ela aplicacion

![image](https://github.com/user-attachments/assets/6d8885ec-db1d-4804-b34d-92192c4de4f7)

# Endpoit que permite verificar la disponibilidad de los servicios ya sean generales o de odontologia
![image](https://github.com/user-attachments/assets/d6365387-7237-4bc9-a638-638f423e3b79)

# Endpoint que permite seleccionar y reservar la cita
![image](https://github.com/user-attachments/assets/901d8beb-df19-4e7b-86fe-be09e6b55120)



