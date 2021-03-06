﻿CREATE TABLE [dbo].[Reservation] (
    [IdReservation]   INT            IDENTITY (1, 1) NOT NULL,
    [DateReservation] DATE           NOT NULL,
    [ClientFirstname] VARCHAR (50)   NOT NULL,
    [ClientLastname]  VARCHAR (50)   NOT NULL,
    [DateStart]       DATE           NOT NULL,
    [DateEnd]         DATE           NOT NULL,
    [TotalPrice]      NUMERIC (7, 2) NOT NULL,
    CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED ([IdReservation] ASC)
);

CREATE TABLE [dbo].[ReservationDetails] (
    [IdReservationDetails] INT            IDENTITY (1, 1) NOT NULL,
    [IdReservation]        INT            NOT NULL,
    [IdRoom]               INT            NOT NULL,
    [RoomPrice]            NUMERIC (5, 2) NOT NULL,
    [Increase]             NUMERIC (5, 2) NULL,
    CONSTRAINT [PK_ReservationDetails] PRIMARY KEY CLUSTERED ([IdReservationDetails] ASC),
    CONSTRAINT [FK_ReservationD_Room] FOREIGN KEY ([IdRoom]) REFERENCES [dbo].[Room] ([IdRoom]),
    CONSTRAINT [FK_ReservationD_Reservation] FOREIGN KEY ([IdReservation]) REFERENCES [dbo].[Reservation] ([IdReservation])
);