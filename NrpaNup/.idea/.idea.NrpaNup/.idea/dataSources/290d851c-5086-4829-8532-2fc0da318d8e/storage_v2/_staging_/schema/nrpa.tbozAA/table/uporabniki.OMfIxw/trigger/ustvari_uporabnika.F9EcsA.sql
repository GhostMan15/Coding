
DELIMITER //

CREATE TRIGGER dodaj_zaposlenega
    AFTER INSERT ON uporabniki
    FOR EACH ROW
BEGIN
    IF NEW.vrsta_uporabnika = 'zaposlen' THEN
        INSERT INTO zaposlen (id_zaposlen, ime, geslo)
        VALUES (NEW.id_uporabnika, NEW.ime, NEW.geslo);
    END IF;
END;
//

DELIMITER ;