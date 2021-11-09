"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
exports.__esModule = true;
require("./GroupDetailsPage.scss");
var react_1 = require("react");
var CardsList_1 = require("./components/cardsList/CardsList");
var GroupDetails_1 = require("./components/groupDetails/GroupDetails");
var react_redux_1 = require("react-redux");
var selectors_1 = require("store/cards/selectors");
var react_router_1 = require("react-router");
var actions_1 = require("store/cards/actions");
var CardForm_1 = require("./components/cardForm/CardForm");
function GroupDetailsPage() {
    var groupId = react_router_1.useParams().groupId;
    var dispatch = react_redux_1.useDispatch();
    var isLoading = react_redux_1.useSelector(selectors_1.selectIsLoading);
    var cards = react_redux_1.useSelector(selectors_1.selectCards);
    var groupDetails = react_redux_1.useSelector(selectors_1.selectGroupDetails);
    var selectedItem = react_redux_1.useSelector(selectors_1.selectSelectedCard);
    react_1.useEffect(function () {
        dispatch(actions_1.getCards(groupId));
    }, [groupId, dispatch]);
    var onItemSelected = function (item) {
        dispatch(actions_1.selectCard(item));
    };
    var onFormSubmit = function (item) {
        var card = !selectedItem
            ? { back: {}, front: {} }
            : __assign({}, selectedItem);
        card.front.value = item.frontValue;
        card.front.example = item.frontExample;
        card.front.isUsed = item.frontEnabled;
        card.back.value = item.backValue;
        card.back.example = item.backExample;
        card.back.isUsed = item.backEnabled;
        card.comment = item.comment;
        if (card.id) {
            dispatch(actions_1.updateCard(card));
        }
        else {
            dispatch(actions_1.addCard(card));
        }
    };
    var onDelete = function () {
        dispatch(deleteCard());
    };
    if (isLoading) {
        return React.createElement("div", null, "Loading...");
    }
    return (React.createElement(React.Fragment, null,
        React.createElement("div", { id: "group-details" },
            React.createElement(GroupDetails_1["default"], { name: groupDetails.name, front: groupDetails.language1, back: groupDetails.language2 }),
            React.createElement(CardForm_1["default"], { card: selectedItem, onSubmit: onFormSubmit, onDelete: true })),
        React.createElement(CardsList_1["default"], { cards: cards, onItemSelected: onItemSelected })));
}
exports["default"] = GroupDetailsPage;
