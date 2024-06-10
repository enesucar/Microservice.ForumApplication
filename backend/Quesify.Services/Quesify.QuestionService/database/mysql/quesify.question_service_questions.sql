-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: quesify.question_service
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questions` (
  `id` varchar(36) NOT NULL,
  `title` varchar(256) NOT NULL,
  `body` text NOT NULL,
  `user_id` varchar(36) NOT NULL,
  `score` int NOT NULL,
  `creation_date` datetime NOT NULL,
  `modification_date` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `title_UNIQUE` (`title`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES ('5c73afa8-cfa5-ee11-828e-202b20d3c2e1','Virtualization: Blazor WASM vs Blazor Server','Why does it take ~15 seconds to display 50,000 records from a Web API in Blazor WASM, but <2 seconds in Blazor Server? Using virtualization in both cases. Is there something that should be made for the WASM case?','7f22bc17-cfa5-ee11-828e-202b20d3c2e1',2,'2023-12-28 22:23:01',NULL),('6a7caee3-d5a5-ee11-828e-202b20d3c2e1','Virtualization Server','The bounty expires in 19 hours. Answers to this question are eligible for a +50 reputation bounty. Peter Thompson wants to draw more attention to this question.\r\nI am trying to write an R shiny app that, given a directory of image files, takes user input and decides which images to display on the page. The number of images may vary depending on the user\'s input. I have chosen to reach this goal using a \"lookup table\", a CSV file that contains the name of all the image file paths as one column and a filtering column that matches the user\'s input. I have a directory called \"IMAGES_\" with three PNG files, and my lookup table','a9e641b1-cea5-ee11-828e-202b20d3c2e1',1,'2023-12-28 23:07:37',NULL),('b52e0bda-cfa5-ee11-828e-202b20d3c2e1','Boost serialize for std::basic_string with custom allocator','This seemed to work fine till I tried to serialize a string which had a space in it.\r\n\r\nSerialization is done into a boost::archive::text_oarchive and the separator is set to space. So after deserialization only the first part of the string got read from the archive e.g. I wrote \"Hello World\" to the archive but only got \"Hello\" after deserialization. For std::string boost adds a length field before the text. This is not the case for the custom string.','7f22bc17-cfa5-ee11-828e-202b20d3c2e1',0,'2023-12-28 22:24:24',NULL);
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-29  3:00:19
