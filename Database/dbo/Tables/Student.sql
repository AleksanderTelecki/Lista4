CREATE TABLE [dbo].[Student] (
    [id_stud]       VARCHAR (36) NOT NULL,
    [SURNAME]       VARCHAR (20) NOT NULL,
    [NAME]          VARCHAR (20) NOT NULL,
    [BIRTHDATE]     DATE         NOT NULL,
    [AGE]           INT          NOT NULL,
    [ADRESS_CITY]   VARCHAR (20) NOT NULL,
    [ADRESS_STREET] VARCHAR (20) NOT NULL,
    [PESEL]         VARCHAR (11) NOT NULL,
    [NR_ALBUM]      VARCHAR (6)  NOT NULL,
    [STUDENT_IMAGE] IMAGE        NOT NULL,
    PRIMARY KEY CLUSTERED ([id_stud] ASC)
);

