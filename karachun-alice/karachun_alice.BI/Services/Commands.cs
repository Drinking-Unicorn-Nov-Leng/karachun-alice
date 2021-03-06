using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using karachun_alice.BI.Interfaces;
using karachun_alice.Data.Dto;
using Yandex.Alice.Sdk.Models;

namespace karachun_alice.BI.Services
{
    public class Commands : ICommands
    {

        private List<Command> CommandsList = new List<Command>();
        private readonly Command _startCommand;
        private readonly Command _closeCommand;

        public Commands()
        {
            var start = new Command("Старт")
            {
                Active = Start
            };

            CommandsList.Add(start);
            _startCommand = start;

            var close = new Command("Назад")
            {
                Active = Start
            };
            CommandsList.Add(close);
            _closeCommand = close;

            CommandsList.Add(new Command("Расскажи сказку", start, close)
            {
                Active = GetLegend
            });
            CommandsList.Add(new Command("Куда пойти в Новгороде", start, close)
            {
                Active = WhereGoInNovgorod
            });
            CommandsList.Add(new Command("Расскажи ещё сказку", CommandsList[2], close)
            {
                Active = GetLegend
            });
            CommandsList[CommandsList.Count - 1].AddInSubCommandYourself();
            CommandsList.Add(new Command("Куда ещё пойти в Новгороде", CommandsList[3], close)
            {
                Active = WhereGoInNovgorod
            });
            CommandsList[CommandsList.Count - 1].AddInSubCommandYourself();
            CommandsList.Add(new Command("Хочу больше!", new List<Command>() { CommandsList[3], start }, close)
            {
                Active = GetApp
            });
        }

        public async Task<AliceResponseDto> Execute(string command) =>
             CommandsList.FirstOrDefault(x => x.CommandText.ToLower() == command.ToLower())?.Active.Invoke() ?? Error();

        private AliceResponseDto Start() => new AliceResponseDto()
        {
            Text = "Расскажу вам сказку я ребятки про торча и шоколадки",
            Buttons = CommandsList[0].GetButtons()
                        .Select(command => new AliceButtonModel(command, true)).ToList()
        };

        private AliceResponseDto GetLegend() => new AliceResponseDto()
        {
            Text = GetRandomLegend(),
            Buttons = CommandsList[2]?.GetButtons().Select(command => new AliceButtonModel(command, true)).ToList()
        };

        private AliceResponseDto GetApp() => new AliceResponseDto()
        {
            Text = "Хотите посетить больше интересных мест и погрузиться в фольклёр нашего региона? Скачайте наше приложение: http://95.142.47.217:2867/Attachment/100",
            Buttons = CommandsList[2]?.GetButtons().Select(command => new AliceButtonModel(command, true)).ToList()
        };

        private AliceResponseDto WhereGoInNovgorod() => new AliceResponseDto()
        {
            Text = GetRandomPoint(),
            Buttons = CommandsList[3]?.GetButtons().Select(command => new AliceButtonModel(command, true)).ToList()
        };

        private string GetRandomLegend()
        {
            var legends = new string[]
            {
                @" Жили курочка с кочетком, и пошли они в лес по орехи. Пришли к
орешне, кочеток залез на орешню рвать орехи, а курочку он оставил на
земле подбирать орехи: кочеток кидает, а курочка подбирает. Вот кинул
кочеток орешек, и попал курочке в глазок, и вышиб глазок. Курочка по-
шла – плачет. Вот едут бояре и спрашивают:
   – Курочка, курочка, что ты плачешь?
   – Мне кочеток вышиб глазок.
   – Кочеток, кочеток, на что ты курочке вышиб глазок?
   – Мне орешня портки разорвала!
   – Орешня, орешня, на что ты кочетку портки разодрала?
   – Меня козы поглодали!
   – Козы, козы, на что вы орешню поглодали?
   – Нас пастухи не берегут!
   – Пастухи, пастухи, что вы коз не бережёте?
   – Нас хозяйка блинами не кормит!
   – Хозяйка, хозяйка, что ты пастухов блинами не кормишь?
   – У меня свинья опару пролила!
   – Свинья, свинья, на что ты у хозяйки опару пролила?
   – У меня волк поросёнка унёс!
   – Волк, волк, на что ты поросёнка унёс?
   – Я есть захотел, мне Бог повелел!",
                @"Жили дед и баба, да был у них внучок Ваня. Ваня и говорит:
   – Деду, деду, купи мне лодку, я пойду рыбку ловить.
   Лове, лове рыбку, налове полну лодку да и плывет до берегу. Приплы-
вет и отдает дедке да бабке. А бабушка, когда придеть, да и Ваню зовет:
– Приплынь, приплынь до бережка, твоя бабушка пришла, тебе исте
принесла, принесла и сороченьку белэсэньку, и ложечку краснэсеньку.
   Ваня приплывает, бабушка рыбку заберет.
   Вот услышала ведьма, что бабушка Ваню так зовет, и сдумакла сама
позвать:
   – Сынку, сынку, Ванечка! Приплынь до бережка, твоя матенька при-
шла, тебе есте принесла, сороченьку бэлэсэньку и ложечку краснэсэньку.
   А Ваня думал правда, да приплыл, а она его схватила, да в мешок за
плечи, да и понесла. Несла, несла да и уморилася, да и села отдохнуть,
да и придремала. Ваня вылез тихонько с мешечка, а в мешечек кирпичу
наклал, завязал, а сам убежал.
   Вот ведьма проснулась, взяла мешок и понесла домой с кирпичами.
А Ванька залез на дерево и сидит на дереве высоко-высоко. Ведьма как
прибежала, увидела Ваню и давай дерево грызти. Перегрызла дерево,
Ваня перепрыгнул на друго дерево. Она опять перегрызла дерево, Ваня
опять перепрыгнул. Она опять перегрызла дерево. Ну, тут Ваня чего де-
лать, летят гуси-лебеди, он давай проситься:
   – Гуси-лебеди, возьмите меня на крылята да понесите до маво ба-
теньки да матеньки, там всего много – исте, пите довольно.
   Гуси взяли его на крыла, да и понесли домой, да и посадили его на дом.
Вот тут отец его да мать собрали на столи все, пирожки. Дадуть и говорять:
   – Тебе пирожок, тебе пирожок, тебе пирожок.
   А Ваня сидит на чердаку и говорит:
   – А мне пирожок?
   – Тише, тише, вроде Ваня отзывается?! Тебе ложечку, тебе ложечку,
тебе ложечку.
   А Ваня сидит на чердаку:
   – А мини ложечку?
   Вот тут дед с бабкой услыхали, ну и пошли забрали Ваню домой.",
                @"Заблудился мужик и пришел в избушку переночевать. Вечером при-
ходит Баба-Яга Одноглазая, пригоняет овец в избу. И вдруг видит мужи-
ка. Посадила его и спрашивает:
    – Что ты умеешь делать?
    Он отвечает:
    – Я кузнец.
    – Скуй мне глаз.
    – Ладно, – отвечает мужик, – затопляй печку, неси веревки. 
Связал ее, накалил железину и наставил на здоровый глаз. У Бабы-Яги
глаз и выскочил. Она разгневалась, веревки все перервала и кричит:
   – Теперь ты от меня не уйдешь!
   Села на порог, мужику бедному не выйти. Так всю ночь и просидела.
А наутро Яга стала выпускать баранов пастись, на ощупь, так как ниче-
го не видит. Мужик, не будь дураком, взял шубу, вывернул кверху шерс-
тью, руки в рукава просунул и пополз на четвереньках. Яга пощупала,
подумала, что баран и тоже выкинула, как и остальных.
   Мужик перекрестился и говорит:
   – Вот так лихо – спать хошь – не спишь. Вот это и есть хошь – не
ешь. Лихо!",
                @"Две девушки купалися, а к ним гад сел на платье. Они вышли из
воды и говорят:
   – У меня на платье гад сидит.
   – Да возьми палку и спихни.
   А гад говорит:
   – Не отдам платье. Замуж пойдешь?
   – Нет.
   А подруга учит обману:
   – Скажи нарочно, что пойдешь.
   Ну, она и сказала:
   – Ладно, пойду.
   Гад и уполз за кусты. Домой пришла и говорит матери, что это мне
гад на платье сел и говорит:
   – Пойдешь замуж?
   А мать спрашивает:
   – Чего ты сказала?
– Сказала нарочно, что пойду.
   – Дура, оставила бы платье.
   В окошко глянули: гады ползут на свадьбу. Они ворота закрыли, а
гады окошко разбили и утащили ее. А через пять годов пришла к мате-
ри. Уж двое ребят у нее. Мать спрашивает:
   – Где ты живешь?
   – Да в воды. Мужика Осипом зовут.
   – Как ты вернешься?
   – Да скажу: «Осип, Осип, выдь сюды». Он и выйдет и возьмет с собой.
   Дочка спать легла, а мать говорит, отсюда ей не уйти, и побежала к реке.
   – Осип, Осип, выдь за мной.
   Он вышел, а она ему голову и отсекла. Дочка утром встает и говорит:
   – Мама, что-то сердце заболело. Надо домой идти.
   Дочь и пошла до воды.
   – Осип, Осип, выдь сюды.
   Он не выходит. Видит дочь – кровь на воды, и поняла она всё. И говорит:
   – Мать вот как сделала. И буду я серой кукушечкой, а вы, детуш-
ки, – пташечками."
            };

            return legends[new Random().Next(legends.Length - 1)];
        }

        private string GetRandomPoint()
        {
            var points = new string[]
            {
                "Советуем посетить знаменитый Центр музыкальных древностей В.И. Поветкина!",
                "Настоящим сокровищем будет музей музей изобразительнных искусств!",
                "Обязательно посятите башню Кокуй!",
                "Настоятельно рекомендуем посетить музей утюга",
                "Новгородская печатня - вот план на выходной день!",
                "Советуем сфотографироваться с памятником девушки туристки",
                "Обнимите медведя на скамейке! Не знаете где найти? У любого новгородчанина спросите!",
                "Перед походом к памятнику семейства мойдодыров обязательно помойтесь!"
            };

            return points[new Random().Next(points.Length - 1)];
        }

        private AliceResponseDto Error() => new AliceResponseDto()
        {
            Text = @"Команда не найдена :( Попробуй одну из команл ниже!",
            Buttons = _startCommand.GetButtons().Select(command => new AliceButtonModel(command, true)).ToList()
        };
    }
}
