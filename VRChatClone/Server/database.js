require('dotenv').config();
const mongoose = require('mongoose');

// Connect to MongoDB
mongoose.connect(process.env.MONGO_URI, {
    useNewUrlParser: true,
    useUnifiedTopology: true,
})
.then(() => console.log("✅ MongoDB Connected"))
.catch(err => console.log("❌ MongoDB Connection Error:", err));

// **User Schema**
const UserSchema = new mongoose.Schema({
    firebaseUID: { type: String, required: true, unique: true },
    username: { type: String, required: true },
    vrCoins: { type: Number, default: 0 },
    isBanned: { type: Boolean, default: false },
    inventory: { type: Array, default: [] },
    createdAt: { type: Date, default: Date.now }
});

const User = mongoose.model("User", UserSchema);

// **Fetch User Profile**
async function getUserProfile(userId) {
    try {
        const user = await User.findOne({ firebaseUID: userId });
        return user || null;
    } catch (error) {
        console.error("❌ Error fetching user profile:", error);
        return null;
    }
}

// **Add VRCoins to a User**
async function addVRCoins(userId, amount) {
    try {
        const user = await User.findOneAndUpdate(
            { firebaseUID: userId },
            { $inc: { vrCoins: amount } },
            { new: true }
        );
        return user;
    } catch (error) {
        console.error("❌ Error adding VRCoins:", error);
        return null;
    }
}

// **Deduct VRCoins from a User**
async function deductVRCoins(userId, amount) {
    try {
        const user = await User.findOne({ firebaseUID: userId });
        if (!user || user.vrCoins < amount) {
            return { success: false, message: "Not enough VRCoins!" };
        }
        user.vrCoins -= amount;
        await user.save();
        return { success: true, user };
    } catch (error) {
        console.error("❌ Error deducting VRCoins:", error);
        return { success: false, message: "Transaction failed!" };
    }
}

// **Ban a Player**
async function banUser(userId) {
    try {
        const user = await User.findOneAndUpdate(
            { firebaseUID: userId },
            { isBanned: true },
            { new: true }
        );
        return { success: true, message: `User ${user.username} has been banned.` };
    } catch (error) {
        console.error("❌ Error banning user:", error);
        return { success: false, message: "Ban failed!" };
    }
}

// **Unban a Player**
async function unbanUser(userId) {
    try {
        const user = await User.findOneAndUpdate(
            { firebaseUID: userId },
            { isBanned: false },
            { new: true }
        );
        return { success: true, message: `User ${user.username} has been unbanned.` };
    } catch (error) {
        console.error("❌ Error unbanning user:", error);
        return { success: false, message: "Unban failed!" };
    }
}

// **Add an Item to a Player's Inventory**
async function addItemToInventory(userId, itemName) {
    try {
        const user = await User.findOneAndUpdate(
            { firebaseUID: userId },
            { $push: { inventory: itemName } },
            { new: true }
        );
        return { success: true, user };
    } catch (error) {
        console.error("❌ Error adding item to inventory:", error);
        return { success: false, message: "Inventory update failed!" };
    }
}

// **Export Functions**
module.exports = {
    getUserProfile,
    addVRCoins,
    deductVRCoins,
    banUser,
    unbanUser,
    addItemToInventory
};
