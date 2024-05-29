using STRINGS;

namespace DiseasesExpanded
{
    public class STRINGS
    {
        public class GERMS
        {
            public class ABANDONED
            {
                public static LocString NAME = (LocString)"Abandoned Germs";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Abandoned Germs present.\n";
                public static LocString DESCRIPTION = "Those germs no longer exist, and if they do, they will die quickly.";
            }

            public class BOGINSECTS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Bog Bugs", nameof(BOGINSECTS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Bog Bugs present.\n";
            }

            public class FROSTHARDS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Frost Shards", nameof(FROSTHARDS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Frost Shards present.\n";
            }

            public class GASSYGERMS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Gassy Germs", nameof(GASSYGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"GassyGerms present\n";
            }

            public class HUNGERGERMS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Hungerms", nameof(HUNGERGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Hungerms present\n";
            }

            public class ALIENGERMS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Alien Goo", nameof(ALIENGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Alien Goo present\n";
            }

            public class MEDICALNANOBOTS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Medical Nanobots", nameof(MEDICALNANOBOTS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Medical Nanobots present\n";
                public static LocString DESCRIPTION = (LocString)"Medical Nanobots are engineeded with sole purpose of fighting malicious germs and improving duplicants' health.";

                public static LocString VERSION_PATTERN = (LocString)"Version {ATTACK}/{RESILIANCE}";
                public static LocString VERION_EFFICIENCY_PATTERN = (LocString)"Efficiency: " +
                     "Attributes: <color=#00FF00>{0}</color>, " +
                     "Breathing: <color=#00FF00>{1}</color>, " +
                     "Calories: <color=#00FF00>{2}</color> " +
                     "- Healing: <color=#00FF00>{3}</color>, " +
                     "Energy: <color=#00FF00>{4}</color>, " +
                     "Stress reduction: <color=#00FF00>{5}</color>";
                public static LocString VERSION_RESILIANCE_PATTERN = (LocString)"Resiliance: " +
                    "Germ Resistance: <color=#00FF00>{0}</color>, " +
                    "Duration: <color=#00FF00>{1}</color>, " +
                    "Perpetum Mobile: <color=#00FF00>{2}</color> " +
                    "- Exposure Threshold: <color=#00FF00>{3}</color>, " +
                    "Radiation: <color=#00FF00>{4}</color>, " +
                    "Temperature: <color=#00FF00>{5}</color>";
                public static LocString RADIATION_HELP_PATTERN = (LocString)"Radiation Kill Rate: {0}";
                public static LocString TEMPERATURE_HELP_PATTERN = (LocString)"Optimal Temperature: {0} to {1}";
                public static LocString UPGRADE_HELP_PATTERN = (LocString)"Use {0} to develop nanobots.";
                public static LocString LEGEND_HEADER = (LocString)"Included features:\n";
            }

            public class MUTATINGGERMS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Unstable Virus", nameof(MUTATINGGERMS));
                public static LocString LEGEND_HOVERTEXT = (LocString)"Unstable Virus present\n";

                public static LocString STRAIN_PATTERN = (LocString)"Strain {ATTACK}/{RESILIANCE}";
                public static LocString STRAIN_SEVERITY_PATTERN = (LocString)"Severity: " +
                    "Attributes: <color=#FF0000>{0}</color>, " +
                    "Breathing: <color=#FF0000>{1}</color>, " +
                    "Calories: <color=#FF0000>{2}</color> " +
                    "- Damage: <color=#FF0000>{3}</color>, " +
                    "Exhaustion: <color=#FF0000>{4}</color>, " +
                    "Stress: <color=#FF0000>{5}</color>";
                public static LocString STRAIN_RESILIANCE_PATTERN = (LocString)"Resiliance: " +
                    "Infectibility: <color=#FF0000>{0}</color>, " +
                    "Duration: <color=#FF0000>{1}</color>, " +
                    "Germ Coughing: <color=#FF0000>{2}</color> " +
                    "- Exposure Threshold: <color=#FF0000>{3}</color>, " +
                    "Radiation: <color=#FF0000>{4}</color>, " +
                    "Temperature: <color=#FF0000>{5}</color>";
                public static LocString MUTATION_HELP_PATTERN = (LocString)"Use {0} to mutate the germs in more safe direction.";
                public static LocString TREAT_POTENTIAL_PATTERN = (LocString)"Threat potential: {0}%";
                public static LocString MUTATION_SPEED_PATTERN = (LocString)"Estimated mutation speed: {0:F2} (mutation rate reduction Lvl: {1})";
                public static LocString LEGEND_HEADER = (LocString)"Genoms' values:\n";
            }
        }

        public class DISEASES
        {
            public class BOGSICKNESS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Bog Bugs Infestation", nameof(BOGSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Annoying bites of small Bog Bugs";
                public static LocString POPFXTEXT = (LocString)"Bog Bug Bite!";
                public static LocString DESCRIPTION = (LocString)"Bog Bugs are lured by Bog Bucket sweet scent, but they can also feed on duplicants...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area can result in Bog Bugs bites";
            }

            public class FROSTSICKNESS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Frost Pox", nameof(FROSTSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Pretty, but pretty cold frost patterns on the skin.";
                public static LocString DESCRIPTION = (LocString)"Little shards of frost cover duplicant's skin in a pretty patterns, reducing their resistance for temperature and diseases.";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Frost Pox.";
            }

            public class GASSICKNESS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Moo Flu", nameof(GASSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Makes duplicant fart like a mad Moo.";
                public static LocString DESCRIPTION = (LocString)"Whatever it is in the Gas Grass, it makes everything around fart...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Moo Flu.";
            }

            public class HUNGERSICKNESS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Everlasting Hunger", nameof(HUNGERSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Makes duplicant hungry all the time.";
                public static LocString DESCRIPTION = (LocString)"The hunger of the ravenous plants seems to be infectious...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area causes Everlasting Hunger.";
            }

            public class SPINDLYCURSE
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Sleeping Curse", nameof(SPINDLYCURSE));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Periodically causes duplicant to fall asleep in random places.";
                public static LocString DESCRIPTION = (LocString)"The tale says one duplicant slept 100 cycles after hurting herself on the Spindle, but it is possible she just faked it not to clean the toilets...";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area affected by Sleeping Curse.";
                public static LocString EXPOSURE_INFO = (LocString)"Got hurt by the Spindle of a Grubfruit.\n\nAs a result, this duplicant will fall asleep in random places.\nImprove Farming skill or wear protective equipment to prevent future infections.\nUse Espresso Machine to prevent Narcoleptic Naps.\n";
            }

            public class ALIENSICKNESS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Alien Symbiot", nameof(ALIENSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"Enhances and stresses the duplicant.";
                public static LocString DESCRIPTION = (LocString)"Alien microbes increase duplicant's skills. However, this feels unnatural and makes infected duplicant stressed out. The goo is perfectly adapted for space travels and it will rapidly grow wherever radiation is present.";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area affected by Alien Goo.";
                public static LocString EXPOSURE_INFO = (LocString)"Got exposed to Alien Goo";
            }

            public class MUTATINGSICKNESS
            {
                public static LocString NAME = (LocString)UI.FormatAsLink("Mutating Disease", nameof(MUTATINGSICKNESS));
                public static LocString DESCRIPTIVE_SYMPTOMS = (LocString)"May affect duplicants in many different ways";
                public static LocString DESCRIPTION = (LocString)"Virus causing the disease is very unstable and often mutates. Results may be different each time.";
                public static LocString LEGEND_HOVERTEXT = (LocString)"Area affected by Mutating Disease.";
                public static LocString EXPOSURE_INFO = (LocString)"Got infected by Mutating Virus.";
                public static LocString POPFXTEXT = (LocString)"Painful cough!";
            }
        }

        public class EFFECTS
        {
            public class BOGRECOVERY
            {
                public static LocString NAME = (LocString)"Bog Bugs Infestation Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Bog Bugs Infestation.";
            }

            public class FROSTRECOVERY
            {
                public static LocString NAME = (LocString)"Frost Pox Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Frost Pox.";
            }

            public class GASRECOVERY
            {
                public static LocString NAME = (LocString)"Moo Flu Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Moo Flu.";
            }

            public class HUNGERRECOVERY
            {
                public static LocString NAME = (LocString)"Everlasting Hunger Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Everlasting Hunger.";
            }

            public class SPINDLYRECOVERY
            {
                public static LocString NAME = (LocString)"Sleeping Curse Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Sleeping Curse.";
            }

            public class ALIENRECOVERY
            {
                public static LocString NAME = (LocString)"Alien Symbiot Recovery";
                public static LocString DESC = (LocString)"This duplicant was cured from Alien Symbiot.";
            }

            public class ALIENASSIMILATION
            {
                public static LocString NAME = (LocString)"Assimilation Successful";
                public static LocString DESC = (LocString)"Alien Symbiot completed assimilation and no longer provides benefits to the host.";
            }

            public class MUTATEDSYMPTOMS
            {
                public static LocString NAME = (LocString)"Mutating Disease Symptoms";
                public static LocString DESC = (LocString)"This duplicant is suffering from various symptoms of mutating disease.";
            }

            public class MUTATEDRECOVERY
            {
                public static LocString NAME = (LocString)"Mutating Disease Recovery";
                public static LocString DESC = (LocString)"This duplicant just recovered from Mutating Disease.";
            }

            public class JUSTGOTTESTED
            {
                public static LocString NAME = (LocString)"Just Got Tested";
                public static LocString DESC = (LocString)"This Duplicant just delivered germ sample for futher testing.";
            }

            public class SPINDLYTHORNS
            {
                public static LocString NAME = (LocString)"Spindly Thorns";
                public static LocString DESC = (LocString)"This Plant developed protective thorns, similar to Grub Fruit spindles. Touching it may cause Sleeping Curse...";
            }

            public class NANOBOTENHANCEMENT
            {
                public static LocString NAME = (LocString)"Nanobot Enhancement";
                public static LocString DESC = (LocString)"This Duplicant is getting health boost from friendly Nanobot enhancement.";
            }
        }

        public class CURES
        {
            public class GASCURE
            {
                public static LocString NAME = (LocString)"Sprouting Tablet";
                public static LocString DESC = (LocString)"If a berry sprouts in a duplicant's belly, their farts will smell like flowers";
            }

            public class MUDMASK
            {
                public static LocString NAME = (LocString)"Muddy Salve";
                public static LocString DESC = (LocString)"When covered in mud, duplicants are not so tasty for Bog Bugs.";
            }

            public class SAPSHOT
            {
                public static LocString NAME = (LocString)"Sweet Sap Shot";
                public static LocString DESC = (LocString)"Thick and full of sugar, Sweet Sap Shot condenses many calories into one syringe to quickly satisfy even the most overwhelming hunger.";
            }

            public class RADSHOT
            {
                public static LocString NAME = (LocString)"Sweet Rad Shot";
                public static LocString DESC = (LocString)"Uranium contains about 18 million kCal per gram - enough to quickly satisfy even the most overwhelming hunger.";
            }

            public class ANTIHISTAMINEBOOSTER
            {
                public static LocString NAME = (LocString)"Allergy Immuno Booster";
                public static LocString DESC = (LocString)"Prevents allergic reactions in the future.";
            }

            public class VACCINE
            {
                public static LocString NAME = (LocString)"{0} Vaccine";
                public static LocString DESC = (LocString)"Grants immunity to {0} disease.";
            }

            public class DEEPBREATH
            {
                public static LocString NAME = (LocString)"Deep Breath Serum";
                public static LocString DESC = (LocString)"Slightly improves duplicant's breath and germ resistance.";
            }

            public class SUPERSERUM
            {
                public static LocString NAME = (LocString)"Super Serum";
                public static LocString DESC = (LocString)"Enhances duplicant's attributes and makes them immune to most dangerous pathogens.";
            }

            public class TUMMYSERUM
            {
                public static LocString NAME = (LocString)"Tummy Serum";
                public static LocString DESC = (LocString)"Calms even most angry tummy and makes it immune to subsequent infections.";
            }

            public class YUMMYSERUM
            {
                public static LocString NAME = (LocString)"Yummy Serum";
                public static LocString DESC = (LocString)"Stimulates duplicant's digestive system and makes them immune to most ravenous pathogens.";
            }

            public class MUTATINGANTIVIRAL
            {
                public static LocString NAME = (LocString)"Unstable Antiviral";
                public static LocString DESC = (LocString)"Cures from, and grants immunity to Mutating Disease for a short time.";
            }

            public class ALIENCURE
            {
                public static LocString NAME = "Alien Cure";
                public static LocString DESC = "Discourages Alien Goo from inhabiting the body of affected duplicant.";
            }

            public class HAPPYPILL
            {
                public static LocString NAME = "Happy Pill";
                public static LocString DESC = "Improves overall happines of affectected duplicant but reduces their motivation to perform even most basic tasks.";
            }

            public class SUNBURNCURE
            {
                public static LocString NAME = "Sun Lotion";
                public static LocString DESC = "Cures duplicant from severe Sunburn.";
            }
        }

        public class GERMFLASK
        {
            public static LocString NAME = (LocString)"{0} Flask";
            public static LocString DESC = (LocString)"Contains gathered {0} germs.";
            public static LocString DESC_NOGERM = (LocString)"Contains gathered {0}.";
        }

        public class GERMFLASKSAMPLE
        {
            public static LocString NAME = (LocString)"Test Sample Flask";
            public static LocString DESC = (LocString)"Used to gather germ sample from infected duplicant into a flask for futher testing.";
        }

        public class MEDICALRESEARCH
        {
            public static LocString NAME = (LocString)"Medical Research";
            public static LocString DESC = (LocString)(UI.FormatAsLink("Medical Research", nameof(RESEARCH)) + " is required to unlock medical technologies.\nIt can be conducted by studying germ samples at an " + UI.FormatAsLink("Apothecary", "APOTHECARY") + " or obtained by doctors while tending their patients.");
            public static LocString RECIPEDESC = (LocString)"Unlocks medical technologies.";
        }

        public class CONTROLLEDMUTATION
        {
            public class ATTACK
            {
                public static LocString NAME = (LocString)"Controlled Severity Mutation";
                public static LocString DESC = (LocString)"Conducted in laboratory conditions, forces Unstable Virus to mutate back it's severity genoms to a weaker form.";
                public static LocString RECIPEDESC = (LocString)"Weakens Unstable Virus severity";
            }
            public class ENVIRONMENTAL
            {
                public static LocString NAME = (LocString)"Controlled Environmental Mutation";
                public static LocString DESC = (LocString)"Conducted in laboratory conditions, forces Unstable Virus to mutate back it's environmental genoms to a weaker form.";
                public static LocString RECIPEDESC = (LocString)"Weakens Unstable Virus ability to live in various environments";
            }
            public class RESILIANCE
            {
                public static LocString NAME = (LocString)"Controlled Resiliance Mutation";
                public static LocString DESC = (LocString)"Conducted in laboratory conditions, forces Unstable Virus to mutate back it's resiliance genoms to a weaker form.";
                public static LocString RECIPEDESC = (LocString)"Weakens Unstable Virus resiliance";
            }

            public class MUTATIONRATEREDUCTION
            {
                public static LocString NAME = (LocString)"Mutation Rate Reduction";
                public static LocString DESC = (LocString)"Conducted in laboratory conditions, reduces ability of Unstable Virus to mutate.";
                public static LocString RECIPEDESC = (LocString)"Slows down mutation rate of Unstable Virus.";
            }

        }

        public class NANOBOTDEVELOPMENT
        {
            public class MORENANOBOTS
            {
                public static LocString NAME = (LocString)"Medical Nanobot Swarm";
                public static LocString DESC = (LocString)"Construct and release more Medical Nanobots.";
            }

            public class MORENANOBOTSBOTTLED
            {
                public static LocString NAME = (LocString)"Medical Nanobot Swarm (Bottled)";
                public static LocString DESC = (LocString)"Medical Nanobots stored in a gas container, easy to deploy anywhere in the base.";
            }

            public class STRESS
            {
                public static LocString NAME = (LocString)"Nanobot Stress Relief Module";
                public static LocString DESC = (LocString)"Medical Nanobots will by more efficient while calming down stressed duplicants.";
            }

            public class HEALTH
            {
                public static LocString NAME = (LocString)"Nanobot BioRepair Module";
                public static LocString DESC = (LocString)"Makes Medical Nanobots better at repairing damaged tissues.";
            }

            public class CALORIES
            {
                public static LocString NAME = (LocString)"Nanobot Metabolism++ Module";
                public static LocString DESC = (LocString)"Medical Nanobots will stimulate duplicants' metabolism better to reduce amount of calories required for survival.";
            }

            public class BREATHING
            {
                public static LocString NAME = (LocString)"Nanobot Oxygen Recycle Module";
                public static LocString DESC = (LocString)"Medical Nanobots will recycle some of exhaled CO2 to redirect Oxygen back to duplicant's bloodstream.";
            }

            public class EXHAUSTION
            {
                public static LocString NAME = (LocString)"Nanobot Invigorating Module";
                public static LocString DESC = (LocString)"Medical Nanobots stimulate duplicant's exhaustion levels to make them more rested and invigorated.";
            }

            public class ATTRIBUTES
            {
                public static LocString NAME = (LocString)"Nanobot BioEnhancement Module";
                public static LocString DESC = (LocString)"Medical Nanobots will enhance duplicant's skils and attributes, making them better at all errands.";
            }

            public class SPAWNING
            {
                public static LocString NAME = (LocString)"Nanobot Perpetum Mobile Protocol";
                public static LocString DESC = (LocString)"Medical Nanobots loss rate will be reduced, allowing them to survive longer in the hostile atmosphere.";
            }

            public class TEMPERATURE
            {
                public static LocString NAME = (LocString)"Nanobot Insulation Coating";
                public static LocString DESC = (LocString)"Coats Nanobots with additional insulation layer, allowing them to survive more extreme temperatures.";
            }

            public class RADIATION
            {
                public static LocString NAME = (LocString)"Nanobot Leaden Coating";
                public static LocString DESC = (LocString)"Coats Nanobots with additional lead layer, allowing them to survive more extreme radiation levels.";
            }

            public class THRESHOLD
            {
                public static LocString NAME = (LocString)"Nanobot Integration Protocol";
                public static LocString DESC = (LocString)"Medical Nanobots will enter duplicant's system more easily.";
            }

            public class RESISTANCE
            {
                public static LocString NAME = (LocString)"Nanobot ImmunoBoost Protocol";
                public static LocString DESC = (LocString)"Improves Nanobots' fighting algorithms, increasing germ resistance of affected duplicants.";
            }

            public class DURATION
            {
                public static LocString NAME = (LocString)"Nanobot Operative Optimization";
                public static LocString DESC = (LocString)"Optimizes efficiency with which Nanobots operate in duplicant's system, making thier effects last longer.";
            }
        }

        public class BUILDINGS
        {
            public class GERMCATCHER
            {
                public static LocString NAME = (LocString)"Germcatcher";
                public static LocString DESC = (LocString)"Gathers ambient germs and stores them in the flask for future research at an Apothecary and vaccine production.";
                public static LocString EFFECCT = (LocString)"Germcatchers collect germ samples for medical study and vaccine production.";
            }

            public class VACCINEAPOTHECARY
            {
                public static LocString NAME = (LocString)"Vaccine Apothecary";
                public static LocString DESC = (LocString)"Vaccine Apothecary uses radiation to weaken germs stored in a flasks to produce life-saving vaccines. \n\nIt must be operated by highly-trained medical personel and it is suggested to use radiation protection for the time of vaccine preparation.";
                public static LocString EFFECCT = (LocString)"Vaccines produced here can grant your duplicants immunity for various diseases.";
            }
            public class SHIELDGENERATOR
            {
                public static LocString NAME = (LocString)"Shield Generator";
                public static LocString DESC = (LocString)"Shields the asteroid from cosmic radiation.";
                public static LocString EFFECCT = (LocString)"When powered, generates shield around the asteroid to protect the surface from cosmic radiation. \n\nRequires line of sight to space.";
            }
        }

        public class STATUSITEMS
        {
            public class GATHERING
            {
                public static LocString NAME = (LocString)"Gathering";
                public static LocString TOOLTIP = (LocString)"This building is currently gathering ambient germs.";
                public static LocString PROGRESS = (LocString)" {GERMS} - {PROGRESS}%";
            }

            public class SHIELDPOWERUP
            {
                public static LocString NAME = (LocString)"Shield Status";
                public static LocString TOOLTIP = (LocString)"This building generates protective shield: ";
                public static LocString PROGRESS = (LocString)": {PROGRESS}%";
            }
        }

        public class TRAITS
        {
            public class INSECTALLERGIES
            {
                public static LocString NAME = (LocString)"Insect Allergies";
                public static LocString DESC = (LocString)"Insects bites will cause additional damage to this Duplicant.";
                public static LocString SHORT_DESC = (LocString)"Allergic reaction to insect bites";
                public static LocString SHORT_DESC_TOOLTIP = (LocString)"Insect Allergies make small bites more dangerous.";
            }

            public class NOTWASHINGHANDS
            {
                public static LocString NAME = (LocString)"Filthy";
                public static LocString DESC = (LocString)"This duplicant won't wash their hands.";
                public static LocString SHORT_DESC = (LocString)"This duplicant is not excited with a concept of personal hygiene.";
                public static LocString SHORT_DESC_TOOLTIP = (LocString)"Washing hands is not something that this duplicant is interested with.";
            }
        }

        public class NOTIFICATIONS
        {
            public class VIRUSMUTATED
            {
                public static LocString PATTERN = (LocString)"New virus mutation observed: {0}";
            }

            public class NANOBOTUPGRADE
            {
                public static LocString PATTERN = (LocString)"New nanobot update is ready: {0}";
            }
        }

        public class TAGS
        {
            public class DISPOSABLE_GERMS
            {
                public static LocString PROPER_NAME = "Disposable Germs";
            }
        }

        public class RANDOM_EVENTS
        {
            public class ADOPT_STRAY_PET
            {
                public static LocString NAME = "Adopt Stray Pet";
                public static LocString TOAST = "It was so poor and cute and helpless and we had to adopt it!";
            }

            public class BLOOMING_GRAVES
            {
                public static LocString NAME = "Blooming Graves";
                public static LocString TOAST = "Death cannot be healed. But it can bloom!";
            }

            public class CURSOR_DISINFECTING
            {
                public static LocString NAME = "Disinfecting Cursor";
                public static LocString TOAST = "Your cursor just got upgraded with portable Purification Kit!";
                public static LocString TOAST_END = "Oh no... It's broken...";
            }

            public class CURSOR_RADIOACTIVE
            {
                public static LocString NAME = "Radioactive Cursor";
                public static LocString TOAST = "Your cursor just got upgraded with portable Radiation Emitter! \nIt now glows with around {0} rads!";
                public static LocString TOAST_END = "Oh no... It's broken...";
            }

            public class ERADICATE_GERMS
            {
                public static LocString NAME = "Eradicate Germs";
                public static LocString TOAST = "Most of the germs were eradicated from the environment.";
            }

            public class FILTHY_FOOD
            {
                public static LocString NAME = "Filthy Food";
                public static LocString TOAST = "Your food supply has been infected with food poisoning.";
            }

            public class GREAT_BOG_BUG_MIGRATION
            {
                public static LocString NAME = "Great Migration";
                public static LocString TOAST = "Researchers proclaim cycle of the Bog Bugs. All dwellings increase population.";
                public static LocString TOAST_END = "Swarms of Bog Bugs started migrating away.";
            }

            public class GREAT_SANISHELL_MIGRATION
            {
                public static LocString NAME = "Great Migration";
                public static LocString TOAST = "Researchers proclaim cycle of the Sanishell. All dwellings increase population.";
            }

            public class HUNGRY_PET
            {
                public static LocString NAME = "Hungry Pet";
                public static LocString TOAST = "One of your critters is more hungry than usually. You should probably investigate...";
            }

            public class HURTING_TUMMY
            {
                public static LocString NAME = "Hurting Tummy";
                public static LocString TOAST = "One of your duplicants got hurting tummy. It's nothing good for sure...";
            }

            public class INTENSE_POLLINATION
            {
                public static LocString NAME = "Intense Pollination";
                public static LocString TOAST = "All of the plants released increased amount of pollen.";
            }

            public class INTENSE_RADIATION
            {
                public static LocString NAME = "Intense Radiation";
                public static LocString TOAST = "We noticed increased readings on our radiation-measuring aparature.";
                public static LocString TOAST_END = "Radiation levels seem to be back to normal.";
            }

            public class MANDATORY_TESTING
            {
                public static LocString NAME = "Mandatory Testing";
                public static LocString TOAST = "All duplicants got encouraged to perform mandatory health tests. If one is infected with germs, our tests collect germ samples.";
            }

            public class MIRACULOUS_RECOVERY
            {
                public static LocString NAME = "Miraculous Recovery";
                public static LocString TOAST = "A miracle! Our duplicants are no longer sick!";
            }

            public class NANOBOT_UPDATE
            {
                public static LocString NAME = "Nanobot Update";
                public static LocString TOAST = "New update to Nanobot software got rushed for release! ";
                public static LocString TOAST_MALICIOUS = "Sadly, it may contain some bugs...";
                public static LocString TOAST_END = "Hotfix released: Malicious issues of Nanobot software got fixed.";
            }

            public class NIGHT_OF_THE_LIVING_DEAD
            {
                public static LocString NAME = "Night of the Living Dead";
                public static LocString TOAST = "The end is nigh!";
            }

            public class PANIC_MODE
            {
                public static LocString NAME = "PANIC MODE!!!";
                public static LocString TOAST = "AAAAAAAAAAAAAAAAAAAAAA!!!!!";
            }

            public class PLAGUE_OF_HUNGER
            {
                public static LocString NAME = "Plague of Hunger";
                public static LocString TOAST = "A kilogram of Wheat for a Diamond, and three kilograms of Meal Lice for a Diamond, but do not harm Burgers and Pie!";
            }

            public class PRINT_SOME_GERMS
            {
                public static LocString NAME = "Print Some Germs";
                public static LocString TOAST = "Additional germs were printed from all Printing Pods.";
            }

            public class REGRESSIVE_VIRUS_MUTATION
            {
                public static LocString NAME = "Regressive Mutation";
                public static LocString TOAST = "We observed slightly less dangerous variant of the Virus.";
            }

            public class RESUPPLY_FIRST_AID_KIT
            {
                public static LocString NAME = "Resupply First Aid Kits";
                public static LocString TOAST = "Those medical supplies could be helpful... or better not...";
            }

            public class SLIMY_POLLUTED_OXYGEN
            {
                public static LocString NAME = "Slimy Polluted Oxygen";
                public static LocString TOAST = "All Polluted Oxygen is now even more yucky...";
            }

            public class SPACE_SCREAM
            {
                public static LocString NAME = "In Space No One Can Hear You Scream";
                public static LocString TOAST = "We received strange distress signal from our rocket...";
            }

            public class SPAWN_GERMY_SURPRISE_BOX
            {
                public static LocString NAME = "Surprise Box";
                public static LocString DESC = "Surprise Box DESC";
                public static LocString TOAST_TITLE = "Surprise Box";
                public static LocString TOAST_BODY = "I wonder what is inside?";
            }

            public class SPAWN_INFECTED_ELEMENT
            {
                public static LocString NAME = "Spawn Infected Element";
                public static LocString TOAST = "Spawned some {0} filled with germs.";
            }

            public class SPINDLY_PLANTS
            {
                public static LocString NAME = "Spindly Plants";
                public static LocString TOAST = "Our plants have developed protective spindles. We must be careful not to get hurt...";
            }

            public class SPROUT_FLOWERS
            {
                public static LocString NAME = "Sprout Flowers";
                public static LocString TOAST = "New flower just sprouted!";
                public static LocString TOAST_FAILED = "New flower tried to sprout... but it failed... maybe next time...?";
            }

            public class STRAY_COMET
            {
                public static LocString NAME = "Stray Comet";
                public static LocString TOAST = "Hey, look! A shooting star!";
            }

            public class SUDDEN_PLANT_MUTATION
            {
                public static LocString NAME = "Sudden Mutation";
                public static LocString TOAST = "Some of our plants mutated to have bigger leaves, richer fruits and... is that a tentacle?!?!";
            }

            public class SUDDEN_VIRUS_MUTATION
            {
                public static LocString NAME = "Sudden Mutation";
                public static LocString TOAST = "We observed new mutation strain...";
            }
        }

        public class CODEX
        {
            public class CATEGORY
            {
                public static LocString MEDICINE = UI.FormatAsLink("Medicine", nameof(MEDICINE));
            }
        }

        public class TRANSLATION
        {
            public class AUTHOR
            {
                public static LocString NAME = (LocString)"pether.pg";
            }
        }
    }
}
