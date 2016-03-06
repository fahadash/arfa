ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [TCGame_log], FILENAME = '$(DefaultLogPath)TCGame_log.ldf', SIZE = 1024 KB, MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

