-- MySQL Script generated by MySQL Workbench
-- Fri Jan  5 07:13:26 2024
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema InsuranceDB
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `InsuranceDB` ;

-- -----------------------------------------------------
-- Schema InsuranceDB
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `InsuranceDB` DEFAULT CHARACTER SET utf8 ;
USE `InsuranceDB` ;

-- -----------------------------------------------------
-- Table `InsuranceDB`.`DriverClient`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`DriverClient` (
  `idDriverClient` VARCHAR(100) NOT NULL,
  `nameDriver` VARCHAR(100) NULL,
  `lastNameDriver` VARCHAR(100) NULL,
  `telephoneNumber` VARCHAR(10) NULL,
  `licenseNumber` VARCHAR(30) NULL,
  `password` VARCHAR(100) NULL,
  `age` DATETIME NULL,
  PRIMARY KEY (`idDriverClient`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`VehicleClient`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`VehicleClient` (
  `idVehicleClient` VARCHAR(100) NOT NULL,
  `brand` VARCHAR(45) NULL,
  `color` VARCHAR(45) NULL,
  `model` VARCHAR(45) NULL,
  `plate` VARCHAR(45) NULL,
  `serialNumber` VARCHAR(45) NULL,
  `year` VARCHAR(4) NULL,
  PRIMARY KEY (`idVehicleClient`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`InsurancePolicy`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`InsurancePolicy` (
  `idInsurancePolicy` VARCHAR(100) NOT NULL,
  `startTerm` DATETIME NULL,
  `endTerm` DATETIME NULL,
  `termAmount` INT NULL,
  `price` FLOAT NULL,
  `policyType` VARCHAR(45) NULL,
  `DriverClient_idDriverClient` VARCHAR(100) NOT NULL,
  `VehicleClient_idVehicleClient` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`idInsurancePolicy`),
  CONSTRAINT `fk_InsurancePolicy_DriverClient1`
    FOREIGN KEY (`DriverClient_idDriverClient`)
    REFERENCES `InsuranceDB`.`DriverClient` (`idDriverClient`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_InsurancePolicy_VehicleClient1`
    FOREIGN KEY (`VehicleClient_idVehicleClient`)
    REFERENCES `InsuranceDB`.`VehicleClient` (`idVehicleClient`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE INDEX `fk_InsurancePolicy_DriverClient1_idx` ON `InsuranceDB`.`InsurancePolicy` (`DriverClient_idDriverClient` ASC) VISIBLE;

CREATE INDEX `fk_InsurancePolicy_VehicleClient1_idx` ON `InsuranceDB`.`InsurancePolicy` (`VehicleClient_idVehicleClient` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`Employee`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`Employee` (
  `idEmployee` VARCHAR(100) NOT NULL,
  `nameEmployee` VARCHAR(100) NULL,
  `lastnameEmployee` VARCHAR(100) NULL,
  `username` VARCHAR(45) NULL,
  `password` VARCHAR(45) NULL,
  `rol` VARCHAR(45) NULL,
  `registrationDate` DATETIME NULL,
  PRIMARY KEY (`idEmployee`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`OpinionAdjuster`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`OpinionAdjuster` (
  `idOpinionAdjuster` VARCHAR(100) NOT NULL,
  `creationDate` DATETIME NULL,
  `description` VARCHAR(300) NULL,
  PRIMARY KEY (`idOpinionAdjuster`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`Accident`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`Accident` (
  `idAccident` VARCHAR(100) NOT NULL,
  `location` VARCHAR(100) NULL,
  `longitude` VARCHAR(100) NULL,
  `latitude` VARCHAR(100) NULL,
  `nameLocation` VARCHAR(150) NULL,
  `reportStatus` VARCHAR(45) NULL,
  `accidentDate` DATETIME NULL,
  `DriverClient_idDriverClient` VARCHAR(100) NOT NULL,
  `Employee_idEmployee` VARCHAR(100) NULL,
  `OpinionAdjuster_idOpinionAdjuster` VARCHAR(100) NULL,
  `VehicleClient_idVehicleClient` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`idAccident`),
  CONSTRAINT `fk_ReportAccident_DriverClient1`
    FOREIGN KEY (`DriverClient_idDriverClient`)
    REFERENCES `InsuranceDB`.`DriverClient` (`idDriverClient`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Accident_Employee1`
    FOREIGN KEY (`Employee_idEmployee`)
    REFERENCES `InsuranceDB`.`Employee` (`idEmployee`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Accident_OpinionAdjuster1`
    FOREIGN KEY (`OpinionAdjuster_idOpinionAdjuster`)
    REFERENCES `InsuranceDB`.`OpinionAdjuster` (`idOpinionAdjuster`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Accident_VehicleClient1`
    FOREIGN KEY (`VehicleClient_idVehicleClient`)
    REFERENCES `InsuranceDB`.`VehicleClient` (`idVehicleClient`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE INDEX `fk_ReportAccident_DriverClient1_idx` ON `InsuranceDB`.`Accident` (`DriverClient_idDriverClient` ASC) VISIBLE;

CREATE INDEX `fk_Accident_Employee1_idx` ON `InsuranceDB`.`Accident` (`Employee_idEmployee` ASC) VISIBLE;

CREATE INDEX `fk_Accident_OpinionAdjuster1_idx` ON `InsuranceDB`.`Accident` (`OpinionAdjuster_idOpinionAdjuster` ASC) VISIBLE;

CREATE INDEX `fk_Accident_VehicleClient1_idx` ON `InsuranceDB`.`Accident` (`VehicleClient_idVehicleClient` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`CarInvolved`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`CarInvolved` (
  `idCarInvolved` VARCHAR(100) NOT NULL,
  `brand` VARCHAR(45) NULL,
  `model` VARCHAR(45) NULL,
  `color` VARCHAR(45) NULL,
  `plate` VARCHAR(30) NULL,
  PRIMARY KEY (`idCarInvolved`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`Image`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`Image` (
  `idimages` VARCHAR(100) NOT NULL,
  `imageReport` VARCHAR(4000) NULL,
  `Accident_idAccident` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`idimages`),
  CONSTRAINT `fk_Image_Accident1`
    FOREIGN KEY (`Accident_idAccident`)
    REFERENCES `InsuranceDB`.`Accident` (`idAccident`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE INDEX `fk_Image_Accident1_idx` ON `InsuranceDB`.`Image` (`Accident_idAccident` ASC) VISIBLE;


-- -----------------------------------------------------
-- Table `InsuranceDB`.`Involved`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `InsuranceDB`.`Involved` (
  `idInvolved` VARCHAR(100) NOT NULL,
  `lastNameInvolved` VARCHAR(100) NULL,
  `nameInvolved` VARCHAR(100) NULL,
  `licenseNumber` VARCHAR(100) NULL,
  `Accident_idAccident` VARCHAR(100) NOT NULL,
  `CarInvolved_idCarInvolved` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`idInvolved`),
  CONSTRAINT `fk_Involved_Accident1`
    FOREIGN KEY (`Accident_idAccident`)
    REFERENCES `InsuranceDB`.`Accident` (`idAccident`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Involved_CarInvolved1`
    FOREIGN KEY (`CarInvolved_idCarInvolved`)
    REFERENCES `InsuranceDB`.`CarInvolved` (`idCarInvolved`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

CREATE INDEX `fk_Involved_Accident1_idx` ON `InsuranceDB`.`Involved` (`Accident_idAccident` ASC) VISIBLE;

CREATE INDEX `fk_Involved_CarInvolved1_idx` ON `InsuranceDB`.`Involved` (`CarInvolved_idCarInvolved` ASC) VISIBLE;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
