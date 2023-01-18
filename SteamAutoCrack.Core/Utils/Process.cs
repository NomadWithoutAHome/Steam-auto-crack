﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using SQLitePCL;
using SteamAutoCrack.Core.Config;
using SteamAutoCrack.Core.Utils.SteamAutoCrack.Core.Utils;

namespace SteamAutoCrack.Core.Utils
{
    public interface IProcessor
    {
        public Task ProcessFileGUI();
        public Task ProcessFileCLI();
    }

    public class Processor : IProcessor
    {
        private static readonly ILogger _log = Log.ForContext<Processor>();
        public Processor() { }
        public async Task ProcessFileGUI()
        {
            try
            {
                _log.Information("Starting process...");
                EMUGameInfoConfig eMUGameInfoConfig = null;
                EMUConfig eMUConfigs = null;
                SteamStubUnpackerConfig steamStubUnpackerConfigs = null;
                EMUApplyConfig emuApplyConfigs = null;
                GenCrackOnlyConfig genCrackOnlyConfigs = null;
                if ((Config.Config.ProcessConfigs.Unpack || Config.Config.ProcessConfigs.ApplyEMU || Config.Config.ProcessConfigs.GenerateCrackOnly) && (!File.Exists(Config.Config.InputPath) & !Directory.Exists(Config.Config.InputPath))) 
                {
                    throw new Exception("Invaild input path.");
                }
                if (Config.Config.ProcessConfigs.GenerateEMUGameInfo)
                {
                    eMUGameInfoConfig = Config.Config.EMUGameInfoConfigs.GetEMUGameInfoConfig();
                }
                if (Config.Config.ProcessConfigs.GenerateEMUConfig)
                {
                    eMUConfigs = Config.Config.EMUConfigs.GetEMUConfig();
                }
                if (Config.Config.ProcessConfigs.Unpack)
                {
                    steamStubUnpackerConfigs = Config.Config.SteamStubUnpackerConfigs.GetSteamStubUnpackerConfig();
                }
                if (Config.Config.ProcessConfigs.ApplyEMU)
                {
                    emuApplyConfigs = Config.Config.EMUApplyConfigs.GetEMUApplyConfig();
                    if (!new EMUApply().CheckGoldberg(emuApplyConfigs))
                    {
                        throw new Exception("Goldberg emulator file missing. Please download it in settings.");
                    }
                }
                if (Config.Config.ProcessConfigs.GenerateCrackOnly)
                {
                    genCrackOnlyConfigs = Config.Config.GenCrackOnlyConfigs.GetGenCrackOnlyConfig();
                }

                if (Config.Config.ProcessConfigs.GenerateEMUGameInfo)
                {
                    _log.Information("----- 1. Generate Goldberg Steam Emulator Game Info -----");
                    await new EMUGameInfo().Generate(eMUGameInfoConfig).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.GenerateEMUConfig)
                {
                    _log.Information("----- 2. Generate Goldberg Steam Emulator Configuration -----");
                    await new EMUConfigGenerator().Generate(eMUConfigs).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.Unpack)
                {
                    _log.Information("----- 3. Unpack SteamStub -----");
                    await new SteamStubUnpacker(steamStubUnpackerConfigs).Unpack(Config.Config.InputPath).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.ApplyEMU)
                {
                    _log.Information("----- 4. Apply Goldberg Steam Emulator -----");
                    await new EMUApply().Apply(emuApplyConfigs).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.GenerateCrackOnly)
                {
                    _log.Information("----- 5. Generate Crack Only Files -----");
                    await new GenCrackOnly().Applier(genCrackOnlyConfigs).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.Restore) 
                {
                    _log.Information("----- 6. Restore Crack -----");
                    await new Restore().RestoreFile(Config.Config.InputPath).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                _log.Information("All process completed.");
            }
            catch(Exception ex)
            {
                _log.Error(ex,"Failed to process.");
            }
        }
        public async Task ProcessFileCLI()
        {
            try
            {
                _log.Information("Starting process...");
                EMUGameInfoConfig eMUGameInfoConfig = null;
                EMUConfig eMUConfigs = null;
                SteamStubUnpackerConfig steamStubUnpackerConfigs = null;
                EMUApplyConfig emuApplyConfigs = null;
                GenCrackOnlyConfig genCrackOnlyConfigs = null;
                if ((Config.Config.ProcessConfigs.Unpack || Config.Config.ProcessConfigs.ApplyEMU || Config.Config.ProcessConfigs.GenerateCrackOnly) && (!File.Exists(Config.Config.InputPath) & !Directory.Exists(Config.Config.InputPath)))
                {
                    throw new Exception("Invaild input path.");
                }
                if (Config.Config.ProcessConfigs.GenerateEMUGameInfo)
                {
                    eMUGameInfoConfig = Config.Config.EMUGameInfoConfigs.GetEMUGameInfoConfig();
                }
                if (Config.Config.ProcessConfigs.GenerateEMUConfig)
                {
                    eMUConfigs = Config.Config.EMUConfigs.GetEMUConfig();
                }
                if (Config.Config.ProcessConfigs.Unpack)
                {
                    steamStubUnpackerConfigs = Config.Config.SteamStubUnpackerConfigs.GetSteamStubUnpackerConfig();
                }
                if (Config.Config.ProcessConfigs.ApplyEMU)
                {
                    emuApplyConfigs = Config.Config.EMUApplyConfigs.GetEMUApplyConfig();
                    if (!new EMUApply().CheckGoldberg(emuApplyConfigs))
                    {
                        throw new Exception("Goldberg emulator file missing. Please download it.");
                    }
                }
                if (Config.Config.ProcessConfigs.GenerateCrackOnly)
                {
                    genCrackOnlyConfigs = Config.Config.GenCrackOnlyConfigs.GetGenCrackOnlyConfig();
                }

                if (Config.Config.ProcessConfigs.GenerateEMUGameInfo)
                {
                    _log.Information("----- 1. Generate Goldberg Steam Emulator Game Info -----");
                    await new EMUGameInfo().Generate(eMUGameInfoConfig).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.GenerateEMUConfig)
                {
                    _log.Information("----- 2. Generate Goldberg Steam Emulator Configuration -----");
                    await new EMUConfigGenerator().Generate(eMUConfigs).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.Unpack)
                {
                    _log.Information("----- 3. Unpack SteamStub -----");
                    await new SteamStubUnpacker(steamStubUnpackerConfigs).Unpack(Config.Config.InputPath).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.ApplyEMU)
                {
                    _log.Information("----- 4. Apply Goldberg Steam Emulator -----");
                    await new EMUApply().Apply(emuApplyConfigs).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.GenerateCrackOnly)
                {
                    _log.Information("----- 5. Generate Crack Only Files -----");
                    await new GenCrackOnly().Applier(genCrackOnlyConfigs).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                if (Config.Config.ProcessConfigs.Restore)
                {
                    _log.Information("----- 6. Restore Crack -----");
                    await new Restore().RestoreFile(Config.Config.InputPath).ConfigureAwait(false);
                    _log.Information("--------------------");
                }
                _log.Information("All process completed.");
            }
            catch (Exception ex)
            {
                _log.Error(ex, "Failed to process.");
            }
        }
    }
}
