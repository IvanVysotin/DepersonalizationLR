-- Table: public.combineddata

DROP TABLE IF EXISTS public.combineddata;

CREATE TABLE IF NOT EXISTS public.combineddata
(
    id_combineddata serial not null,
    id_fullnametable integer NOT NULL,
    id_adressdata integer NOT NULL,
    id_gendertable integer NOT NULL,
    CONSTRAINT combineddata_pkey PRIMARY KEY (id_combineddata),
    CONSTRAINT combineddata_id_adressdata_fkey FOREIGN KEY (id_adressdata)
        REFERENCES public.adresstable (id_adressdata) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT combineddata_id_fullnametable_fkey FOREIGN KEY (id_fullnametable)
        REFERENCES public.fullnametable (id_fullnametable) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT combineddata_id_gendertable_fkey FOREIGN KEY (id_gendertable)
        REFERENCES public.gendertable (id_gendertable) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.combineddata
    OWNER to postgres;

-- Table: public.PersonalData

DROP TABLE IF EXISTS public."PersonalData", public.personaldata;

CREATE TABLE IF NOT EXISTS public.personaldata
(
    lastname character varying(50) COLLATE pg_catalog."default" NOT NULL,
    firstname character varying(50) COLLATE pg_catalog."default" NOT NULL,
    surname character varying(50) COLLATE pg_catalog."default",
    sex character varying(7) COLLATE pg_catalog."default" NOT NULL,
    birthdate date,
    phonenumber character varying(18) COLLATE pg_catalog."default",
    registrationddress character varying(255) COLLATE pg_catalog."default"
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."PersonalData"
    OWNER to postgres;

-- Table: public.adresstable

DROP TABLE IF EXISTS public.adresstable;

CREATE TABLE IF NOT EXISTS public.adresstable
(
    id_adressdata serial not null,
    phonenumber character varying(18) COLLATE pg_catalog."default" NOT NULL,
    registrationadress character varying(255) COLLATE pg_catalog."default",
    CONSTRAINT adresstable_pkey PRIMARY KEY (id_adressdata)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.adresstable
    OWNER to postgres;


-- Table: public.fullnametable

DROP TABLE IF EXISTS public.fullnametable;

CREATE TABLE IF NOT EXISTS public.fullnametable
(
    id_fullnametable serial not null,
    lastname character varying(50) COLLATE pg_catalog."default" NOT NULL,
    firstname character varying(50) COLLATE pg_catalog."default" NOT NULL,
    surname character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT fullnametable_pkey PRIMARY KEY (id_fullnametable)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.fullnametable
    OWNER to postgres;


-- Table: public.gendertable

DROP TABLE IF EXISTS public.gendertable;

CREATE TABLE IF NOT EXISTS public.gendertable
(
    id_gendertable serial not null,
    birthdate date NOT NULL,
    sex character varying(7) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT gendertable_pkey PRIMARY KEY (id_gendertable)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.gendertable
    OWNER to postgres;