﻿using ApiIntegratedWithDockerMigrations;
using ApiIntegratedWithDockerMigrations.Migrations;

await MigrationService.ExecuteMigration(args, typeof(CreateCitusExtension).Assembly);