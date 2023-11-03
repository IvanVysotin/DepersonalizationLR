SELECT lastname, firstname, surname, sex, birthdate, phonenumber, registrationddress
	FROM public.personaldata;

SELECT id_gendertable, birthdate, sex
	FROM public.gendertable;

SELECT id_fullnametable, lastname, firstname, surname
	FROM public.fullnametable;

SELECT id_adressdata, phonenumber, registrationadress
	FROM public.adresstable;