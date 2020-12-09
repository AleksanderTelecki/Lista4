CREATE PROCEDURE [dbo].[DeleteStudent]
@id_stud varchar(36)
AS
BEGIN  
            DELETE FROM Student  
            WHERE  id_stud = @id_stud  
END  