const { getUserProfile, addVRCoins, deductVRCoins, banUser, unbanUser, addItemToInventory } = require("./database");

// **Fetch User Profile**
app.get('/profile/:userId', async (req, res) => {
    const user = await getUserProfile(req.params.userId);
    if (!user) return res.status(404).json({ error: "User not found!" });
    res.json(user);
});

// **Buy VRCoins (Stripe)**
app.post('/buy-coins', async (req, res) => {
    const { userId, amount } = req.body;
    const updatedUser = await addVRCoins(userId, amount);
    if (!updatedUser) return res.status(500).json({ error: "Transaction failed!" });
    res.json(updatedUser);
});

// **Spend VRCoins on an Item**
app.post('/spend-coins', async (req, res) => {
    const { userId, amount, itemName } = req.body;
    const transaction = await deductVRCoins(userId, amount);
    if (!transaction.success) return res.status(400).json({ error: transaction.message });

    await addItemToInventory(userId, itemName);
    res.json({ success: true, message: `Purchased ${itemName}!`, user: transaction.user });
});

// **Ban a Player**
app.post('/ban-user', async (req, res) => {
    const result = await banUser(req.body.userId);
    res.json(result);
});

// **Unban a Player**
app.post('/unban-user', async (req, res) => {
    const result = await unbanUser(req.body.userId);
    res.json(result);
});
