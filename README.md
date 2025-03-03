### Ключевые особенности

Перед Вами стоит задача создать рабочий прототип по ТЗ. Основная проблема – написать логику таким образом, чтобы она ложилась на модель клиент-серверного взаимодействия.
Для упрощения работы над тестовым рекомендуется написать клиентскую и серверную логику в виде отдельных структурных элементов, связь через которые можно организовать, например, через интерфейс адаптера.

### Сцена
Сцена представляет собой поле боя, на котором расположены два условных юнита друг напротив друга: юнит игрока и юнит противника.
Над каждым из юнитов должна находиться полоска жизни юнита, на которой будет также отображено количество жизни числом.

### Геймплей

Юниты дерутся между собой, используя разные способности по очереди. Сначала игрок выбирает способность, затем противник, и так до тех пор, пока количество жизни одного из юнитов не опустится ниже нуля.
После чего игра должна перезапуститься заново. Тратить время на проработку ИИ не стоит, достаточно будет, если ИИ будет выбирать действия случайным образом.
Каждый юнит имеет набор из нескольких способностей: атака, барьер, регенерация, огненный шар, очищение. Каждая способность, кроме атаки и очищения, имеет перезарядку и длительность, измеряемые в ходах.
Перезарядка начинается после окончания эффекта. Способность на перезарядке использовать нельзя.

0) Атака – применяется на противника. Наносит 8 единиц урона.
1) Барьер – применяется на себя, блокирует суммарно 5 единиц урона. Длительность 2 хода, перезарядка 4 хода.
2) Регенерация – применяется на себя, каждый ход восстанавливает 2 единицы жизни. Длительность 3 хода, перезарядка 5 ходов.
3) Огненный шар – при использовании наносит противнику 5 урона, после чего накладывается эффект, наносящий каждый ход 1 урона противнику. Длительность 5 ходов, перезарядка 6 ходов.
4) Очищение – применяется на себя, снимает эффект горения. Перезарядка 5 ходов.

### Интерфейс

Должен отображать способности игрока и их перезарядку в ходах. Также в интерфейсе должна быть кнопка перезапуска боя.
Рядом с полосками количества жизни юнитов должны появляться иконки статус-эффектов, полученных после применения способностей.
На иконках эффектов стоит разместить счетчик оставшегося количества ходов, в течение которых эффект будет действовать.
