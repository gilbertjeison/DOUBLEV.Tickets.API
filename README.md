# DOUBLEV.Tickets.API
Api implementada bajo el enfoque Domain Driven Design, basada en experiencia de proyectos creados y trabajados.

Motor de base de datos utilizado es SQL SERVER. 

Generación de entidades desde base de datos con el siguiente comando:

`Scaffold-DbContext "Server=localhost;Database=tickets;Trusted_Connection=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities`

### - Net Core V 6.0
### - SQL SERVER DEVELOPER V 2022

Imagen de referencia de arquitectura a excepción de la capa de presentación que tiene alcance sólo hasta los controladores

![image](https://user-images.githubusercontent.com/25182130/219456241-e1508f13-6db3-4fa0-913b-4285951dfade.png)
