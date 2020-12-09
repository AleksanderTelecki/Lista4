CREATE PROCEDURE [dbo].[ChangeStudent]
@id_stud varchar(36),
@SURNAME varchar(20),
@NAME varchar(20),
@BIRTHDATE Date,
@AGE int,
@ADRESS_CITY varchar(20),
@ADRESS_STREET varchar(20),
@PESEL varchar(11),
@NR_ALBUM varchar(6),
@STUDENT_IMAGE IMAGE
AS
BEGIN  
            UPDATE Student  
            SET    NAME = @NAME,  
                   SURNAME = @SURNAME,  
                   BIRTHDATE = @BIRTHDATE,  
                   ADRESS_CITY = @ADRESS_CITY,
				   ADRESS_STREET = @ADRESS_STREET,
				   AGE = @AGE,
				   PESEL = @PESEL,
				   NR_ALBUM=@NR_ALBUM,
				   STUDENT_IMAGE=@STUDENT_IMAGE
            WHERE  id_stud = @id_stud  
END  